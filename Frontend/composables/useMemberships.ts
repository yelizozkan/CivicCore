import { MembershipStatus, type Membership, type ApiResponse } from '~/types'

//#region API Endpoints
const MEMBERSHIP_ENDPOINTS = {
    BASE: '/api/memberships',
    BY_ID: (id: number) => `/api/memberships/${id}`,
    BY_GROUP: (groupId: number) => `/api/memberships/group/${groupId}`,
    BY_TENANT: (tenantId: number) => `/api/memberships/tenant/${tenantId}`,
    BY_STATUS: (status: string) => `/api/memberships/by-status/${status}`,
    UPDATE_STATUS: (id: number) => `/api/memberships/${id}/status`,
    WAITING_PAYMENT: '/api/memberships/waiting-payment',
    OVERDUE_PAYMENTS: '/api/memberships/overdue-payments',
    // Public endpoints
    RESOLVE_SLUG: (slug: string) => `/api/public/resolve/${slug}`
}
//#endregion

//#region Types
export interface CreateMembershipDto {
    firstName: string
    lastName: string
    identityNumber: string
    motherName: string
    fatherName: string
    birthPlace: string
    birthDate: string
    bloodType: string
    educationLevel: string
    profession: string
    workplaceName?: string | null
    workplacePosition?: string | null
    mobilePhone: string
    workPhone?: string | null
    email: string
    residenceAddress: string
    workplaceAddress?: string | null
    motivationText: string
    kvkkAccepted: boolean
    explicitConsentAccepted: boolean
}

export interface CreateMembershipCommand {
    tenantGroupId: number
    membership: CreateMembershipDto
}

export interface SlugResolveResponse {
    type: 'tenant' | 'group'
    tenantId: number
    tenantGroupId: number | null
    name: string
    tenantName: string
    slug: string
}

export interface UpdateMembershipStatusDto {
    membershipId: number
    newStatus: MembershipStatus
    reason?: string
    approvedById?: number
}
//#endregion

export const useMemberships = () => {
    const api = useApi()
    const config = useRuntimeConfig()

    //#region Public Methods (No Auth Required)

    /**
     * Resolve slug to get tenant/group information
     * Public endpoint - No authentication required
     */
    const resolveSlug = async (slug: string): Promise<SlugResolveResponse> => {
        try {
            const response = await $fetch<SlugResolveResponse>(
                `${config.public.apiBase}${MEMBERSHIP_ENDPOINTS.RESOLVE_SLUG(slug)}`,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json'
                    }
                }
            )

            console.log('Slug resolved:', response)
            return response
        } catch (error: any) {
            console.error('Resolve slug error:', error)
            throw error
        }
    }

    /**
     * Create new membership application
     * Public endpoint - No authentication required
     */
    const createMembership = async (data: CreateMembershipDto, tenantGroupId: number): Promise<Membership> => {
        try {
            const payload: CreateMembershipCommand = {
                tenantGroupId: Number(tenantGroupId),
                membership: data
            }

            console.log('Sending membership payload:', payload)

            const response = await $fetch<Membership>(
                `${config.public.apiBase}${MEMBERSHIP_ENDPOINTS.BASE}`,
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    },
                    body: payload
                }
            )

            console.log('Membership created successfully:', response)
            return response
        } catch (error: any) {
            console.error('Create membership error:', error)
            console.error('Error response:', error.data)
            throw error
        }
    }
    //#endregion

    //#region Auth Required Methods

    /**
     * Get membership by ID
     */
    const getMembershipById = async (id: number): Promise<Membership | null> => {
        try {
            console.log('Calling API: GET /api/memberships/' + id)
            const response = await api.get<any>(`/api/memberships/${id}`)
            console.log('API Response data:', response.data)
            return response.data !== undefined ? response.data : response
        } catch (error) {
            console.error('Get membership by ID error:', error)
            return null
        }
    }

    /**
     * Get memberships by group
     */
    const getMembershipsByGroup = async (groupId: number, status?: MembershipStatus): Promise<Membership[]> => {
        try {
            let url = MEMBERSHIP_ENDPOINTS.BY_GROUP(groupId)
            if (status !== undefined) {
                url += `?status=${status}`
            }
            const response = await api.get<Membership[]>(url)
            return response.data as Membership[]
        } catch (error) {
            console.error('Get memberships by group error:', error)
            throw error
        }
    }

    /**
     * Get memberships by tenant
     */
    const getMembershipsByTenant = async (tenantId: number, status?: MembershipStatus): Promise<Membership[]> => {
        try {
            let url = MEMBERSHIP_ENDPOINTS.BY_TENANT(tenantId)
            if (status !== undefined) {
                url += `?status=${status}`
            }
            const response = await api.get<any>(url)
            return (Array.isArray(response) ? response : response?.data || response) as Membership[]
        } catch (error) {
            console.error('Get memberships by tenant error:', error)
            throw error
        }
    }

    /**
     * Get memberships by status
     */
    const getMembershipsByStatus = async (status: MembershipStatus): Promise<Membership[]> => {
        try {
            const response = await api.get<Membership[]>(MEMBERSHIP_ENDPOINTS.BY_STATUS(status))
            return response.data as Membership[]
        } catch (error) {
            console.error('Get memberships by status error:', error)
            throw error
        }
    }

    /**
     * Update membership status (Admin)
     */
    const updateMembershipStatus = async (data: UpdateMembershipStatusDto): Promise<Membership> => {
        try {
            const response = await api.put<Membership>(
                MEMBERSHIP_ENDPOINTS.UPDATE_STATUS(data.membershipId),
                data
            )
            return response.data as Membership
        } catch (error) {
            console.error('Update membership status error:', error)
            throw error
        }
    }

    /**
     * Delete membership
     */
    const deleteMembership = async (id: number): Promise<void> => {
        try {
            await api.delete(MEMBERSHIP_ENDPOINTS.BY_ID(id))
        } catch (error) {
            console.error('Delete membership error:', error)
            throw error
        }
    }

    /**
     * Get memberships waiting for payment
     */
    const getMembershipsWaitingPayment = async (): Promise<Membership[]> => {
        try {
            const response = await api.get<Membership[]>(MEMBERSHIP_ENDPOINTS.WAITING_PAYMENT)
            return response.data as Membership[]
        } catch (error) {
            console.error('Get memberships waiting payment error:', error)
            throw error
        }
    }

    /**
     * Get memberships with overdue payments
     */
    const getMembershipsOverduePayment = async (): Promise<Membership[]> => {
        try {
            const response = await api.get<Membership[]>(MEMBERSHIP_ENDPOINTS.OVERDUE_PAYMENTS)
            return response.data as Membership[]
        } catch (error) {
            console.error('Get memberships overdue payment error:', error)
            throw error
        }
    }
    //#endregion

    return {
        // Public (No Auth)
        resolveSlug,
        createMembership,

        // Auth Required
        getMembershipById,
        getMembershipsByGroup,
        getMembershipsByTenant,
        getMembershipsByStatus,
        updateMembershipStatus,
        deleteMembership,
        getMembershipsWaitingPayment,
        getMembershipsOverduePayment,

        // Enums & Endpoints
        MembershipStatus,
        MEMBERSHIP_ENDPOINTS
    }
}