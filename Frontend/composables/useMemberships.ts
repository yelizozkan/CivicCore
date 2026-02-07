import type { ApiResponse } from '~/types'

//#region API Endpoints
const MEMBERSHIP_ENDPOINTS = {
    BASE: '/api/memberships',
    BY_ID: (id: number) => `/api/memberships/${id}`,
    BY_GROUP: (groupId: number) => `/api/memberships/group/${groupId}`,
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

export interface MembershipDto {
    id: number
    firstName: string
    lastName: string
    email: string
    mobilePhone: string
    status: MembershipStatus
    createdAt: string
    updatedAt?: string
}

export interface SlugResolveResponse {
    type: 'tenant' | 'group'
    tenantId: number
    tenantGroupId: number | null
    name: string
    tenantName: string
    slug: string
}

export enum MembershipStatus {
    Pending = 'Pending',
    PreApproved = 'PreApproved',
    Approved = 'Approved',
    Rejected = 'Rejected',
    Suspended = 'Suspended',
    Cancelled = 'Cancelled'
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
    const createMembership = async (data: CreateMembershipDto, tenantGroupId: number): Promise<MembershipDto> => {
        try {
            const payload: CreateMembershipCommand = {
                tenantGroupId: tenantGroupId,
                membership: data
            }

            console.log('Sending membership payload:', payload)

            const response = await $fetch<MembershipDto>(
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
    const getMembershipById = async (id: number): Promise<MembershipDto> => {
        try {
            const response = await api.get<MembershipDto>(MEMBERSHIP_ENDPOINTS.BY_ID(id))
            return response.data as MembershipDto
        } catch (error) {
            console.error('Get membership error:', error)
            throw error
        }
    }

    /**
     * Get memberships by group
     */
    const getMembershipsByGroup = async (groupId: number, status?: MembershipStatus): Promise<MembershipDto[]> => {
        try {
            let url = MEMBERSHIP_ENDPOINTS.BY_GROUP(groupId)
            if (status) {
                url += `?status=${status}`
            }
            const response = await api.get<MembershipDto[]>(url)
            return response.data as MembershipDto[]
        } catch (error) {
            console.error('Get memberships by group error:', error)
            throw error
        }
    }

    /**
     * Get memberships by status
     */
    const getMembershipsByStatus = async (status: MembershipStatus): Promise<MembershipDto[]> => {
        try {
            const response = await api.get<MembershipDto[]>(MEMBERSHIP_ENDPOINTS.BY_STATUS(status))
            return response.data as MembershipDto[]
        } catch (error) {
            console.error('Get memberships by status error:', error)
            throw error
        }
    }

    /**
     * Update membership status (Admin)
     */
    const updateMembershipStatus = async (data: UpdateMembershipStatusDto): Promise<MembershipDto> => {
        try {
            const response = await api.put<MembershipDto>(
                MEMBERSHIP_ENDPOINTS.UPDATE_STATUS(data.membershipId),
                data
            )
            return response.data as MembershipDto
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
    const getMembershipsWaitingPayment = async (): Promise<MembershipDto[]> => {
        try {
            const response = await api.get<MembershipDto[]>(MEMBERSHIP_ENDPOINTS.WAITING_PAYMENT)
            return response.data as MembershipDto[]
        } catch (error) {
            console.error('Get memberships waiting payment error:', error)
            throw error
        }
    }

    /**
     * Get memberships with overdue payments
     */
    const getMembershipsOverduePayment = async (): Promise<MembershipDto[]> => {
        try {
            const response = await api.get<MembershipDto[]>(MEMBERSHIP_ENDPOINTS.OVERDUE_PAYMENTS)
            return response.data as MembershipDto[]
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