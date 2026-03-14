<template>
  <div class="space-y-6">
    <!-- Page Header -->
    <div class="flex items-center justify-between">
      <h1 class="text-[28px] font-bold text-[#1e293b]">Üyeler</h1>
      <button class="px-4 py-2 bg-white border border-gray-300 rounded-lg text-sm font-medium text-gray-700 hover:bg-gray-50 flex items-center gap-2 transition-colors">
        <Icon name="mdi:upload" class="w-5 h-5 text-gray-500" />
        İçe Aktar
      </button>
    </div>

    <!-- Tab Navigation -->
    <div class="border-b border-gray-200">
      <nav class="-mb-px flex space-x-8">
        <button 
          v-for="tab in tabs" 
          :key="tab.value"
          @click="handleTabChange(tab.value)"
          class="whitespace-nowrap pb-4 px-1 border-b-2 font-medium text-sm transition-colors"
          :class="[
            activeTab === tab.value
              ? 'border-green-500 text-green-600 font-bold'
              : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
          ]"
        >
          {{ tab.label }}
        </button>
      </nav>
    </div>

    <!-- Filter Area -->
    <div class="flex flex-col sm:flex-row sm:items-center justify-between gap-4 mt-6">
      <!-- Title & Count Badge -->
      <div class="flex items-center gap-3">
        <h2 class="text-xl font-semibold text-gray-900">Üyeler</h2>
        <span class="px-2.5 py-0.5 rounded-full bg-gray-100 text-gray-600 text-sm font-medium border border-gray-200">
          {{ filteredMembers.length }}
        </span>
      </div>

      <!-- Search & Filters -->
      <div class="flex items-center gap-3 w-full sm:w-auto overflow-x-auto pb-1 sm:pb-0">
        <div class="relative w-full sm:w-64 flex-shrink-0">
          <Icon name="mdi:magnify" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 w-5 h-5" />
          <input 
            v-model="searchQuery"
            type="text" 
            placeholder="Üye ara..." 
            class="w-full pl-10 pr-4 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:ring-1 focus:ring-green-500 focus:border-green-500 bg-white"
          >
        </div>

        <button class="flex items-center gap-2 px-3 py-2 bg-white border border-gray-200 rounded-lg text-sm font-medium text-gray-700 hover:bg-gray-50 whitespace-nowrap">
          Durum
          <Icon name="mdi:chevron-down" class="w-4 h-4 text-gray-500" />
        </button>
        <button class="flex items-center gap-2 px-3 py-2 bg-white border border-gray-200 rounded-lg text-sm font-medium text-gray-700 hover:bg-gray-50 whitespace-nowrap">
          Üyelik Tipi
          <Icon name="mdi:chevron-down" class="w-4 h-4 text-gray-500" />
        </button>
        <button class="flex items-center gap-2 px-3 py-2 bg-white border border-gray-200 rounded-lg text-sm font-medium text-gray-700 hover:bg-gray-50 whitespace-nowrap">
          Tarih
          <Icon name="mdi:chevron-down" class="w-4 h-4 text-gray-500" />
        </button>
      </div>
    </div>

    <!-- Loading State -->
    <div v-if="isLoading" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-5">
      <div v-for="i in 8" :key="i" class="bg-white border border-gray-200 rounded-xl p-5 animate-pulse">
        <div class="flex justify-between items-start mb-4">
          <div class="w-14 h-14 bg-gray-200 rounded-full"></div>
          <div class="w-6 h-6 bg-gray-200 rounded"></div>
        </div>
        <div class="h-4 bg-gray-200 rounded w-3/4 mb-2"></div>
        <div class="h-3 bg-gray-200 rounded w-1/2 mb-4"></div>
        <div class="space-y-2 mb-4">
          <div class="h-3 bg-gray-200 rounded w-5/6"></div>
          <div class="h-3 bg-gray-200 rounded w-4/6"></div>
        </div>
        <div class="flex gap-2">
          <div class="h-9 bg-gray-200 rounded flex-1"></div>
          <div class="h-9 bg-gray-200 rounded flex-1"></div>
        </div>
      </div>
    </div>

    <!-- Error State -->
    <div v-else-if="apiError" class="bg-red-50 border border-red-200 rounded-xl p-12 text-center">
      <div class="w-16 h-16 bg-red-100 rounded-full flex items-center justify-center mx-auto mb-4">
        <Icon name="mdi:alert-circle-outline" class="w-8 h-8 text-red-500" />
      </div>
      <h3 class="text-lg font-medium text-red-900 mb-1">Bağlantı Hatası</h3>
      <p class="text-red-700 text-sm">{{ apiError }}</p>
      <button @click="fetchMembers" class="mt-4 px-4 py-2 bg-white border border-red-300 rounded-lg text-sm font-medium text-red-700 hover:bg-red-50 transition-colors">
        Tekrar Dene
      </button>
    </div>

    <!-- Empty State -->
    <div v-else-if="filteredMembers.length === 0" class="bg-white border border-gray-200 rounded-xl p-12 text-center">
      <div class="w-16 h-16 bg-gray-50 rounded-full flex items-center justify-center mx-auto mb-4">
        <Icon name="mdi:account-search" class="w-8 h-8 text-gray-400" />
      </div>
      <h3 class="text-lg font-medium text-gray-900 mb-1">Henüz üye bulunamadı</h3>
      <p class="text-gray-500 text-sm">Arama kriterlerinize uygun üye bulunmuyor veya sisteme henüz üye eklenmemiş.</p>
    </div>

    <!-- Member Cards Grid -->
    <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-5">
      <div 
        v-for="member in paginatedMembers" 
        :key="member.id"
        class="bg-white border border-[#e5e7eb] rounded-xl p-4 hover:shadow-md transition-shadow duration-200 flex flex-col h-full group relative"
      >
        <!-- Card Header: Avatar & Menu -->
        <div class="flex justify-between items-start mb-2">
          <div class="w-[48px] h-[48px] rounded-full flex items-center justify-center text-[16px] font-medium"
               :class="getAvatarColor(member)">
            {{ getInitials(member.fullName) }}
          </div>
          
          <v-menu location="bottom end">
            <template v-slot:activator="{ props }">
               <button v-bind="props" class="p-1 text-gray-400 hover:bg-gray-100 rounded-md transition-colors w-8 h-8 flex items-center justify-center">
                <Icon name="mdi:dots-horizontal" class="w-5 h-5" />
              </button>
            </template>
            <v-list density="compact" class="text-[13px]">
              <v-list-item title="Görüntüle" @click="navigateToProfile(member.id)"></v-list-item>
              <v-list-item title="Düzenle"></v-list-item>
              <v-list-item title="Sil" base-color="error"></v-list-item>
            </v-list>
          </v-menu>
        </div>

        <!-- Member Info -->
        <div class="mb-3 flex-1">
          <h3 class="text-[15px] font-semibold text-[#1e293b] truncate" :title="member.fullName">
            {{ member.fullName }}
          </h3>
          
          <div class="flex items-center gap-1.5 text-[13px] text-[#64748b] mt-1.5" v-if="member.mobilePhone">
            <Icon name="mdi:phone-outline" class="w-4 h-4 shrink-0 text-gray-400" />
            <span class="truncate">{{ member.mobilePhone }}</span>
          </div>
          
          <div class="flex items-center gap-1.5 text-[13px] text-[#64748b] mt-1" v-if="member.email">
            <Icon name="mdi:email-outline" class="w-4 h-4 shrink-0 text-gray-400" />
            <span class="truncate">{{ member.email }}</span>
          </div>
        </div>

        <!-- Meta Details -->
        <div class="space-y-1.5 mb-5 text-[13px] text-gray-500">
          <div class="flex justify-between items-center bg-gray-50/50 -mx-4 px-4 py-2 border-y border-gray-100 mt-2 mb-3">
            <span class="text-gray-500">Durum:</span>
            <span :class="['px-2 py-0.5 rounded text-[11px] font-medium', statusStyles[member.status]?.class]">
              {{ statusStyles[member.status]?.label || 'Bilinmiyor' }}
            </span>
          </div>
          <div class="flex items-center gap-2" v-if="member.birthDate">
            <Icon name="mdi:cake-variant-outline" class="w-4 h-4 text-gray-400" />
            <span class="text-gray-600">{{ formatDate(member.birthDate) }}</span>
          </div>
        </div>

        <!-- Action Buttons -->
        <div class="grid grid-cols-2 gap-3 mt-auto">
          <button 
            disabled
            class="py-2 px-4 border border-green-200 text-green-600 bg-green-50/50 rounded-lg text-[13px] font-medium hover:bg-green-50 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
          >
            Mesaj
          </button>
          <button 
            @click="navigateToProfile(member.id)"
            class="py-2 px-4 bg-[#1e293b] text-white rounded-lg text-[13px] font-medium hover:bg-gray-800 transition-colors"
          >
             Profil
          </button>
        </div>
      </div>
    </div>

    <!-- Pagination -->
    <div v-if="filteredMembers.length > 0" class="flex flex-col sm:flex-row items-center justify-between text-sm text-gray-500 mt-6 border-t border-gray-200 pt-6">
      <div class="mb-4 sm:mb-0">
        {{ paginationStart }}-{{ paginationEnd }} / {{ filteredMembers.length }} üyeler gösteriliyor
      </div>
      
      <div class="flex items-center gap-1">
        <button 
          @click="currentPage--" 
          :disabled="currentPage === 1"
          class="w-8 h-8 flex items-center justify-center rounded border border-gray-200 hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <Icon name="mdi:chevron-left" class="w-5 h-5" />
        </button>
        
        <button 
          v-for="page in totalPages" 
          :key="page"
          @click="currentPage = page"
          :class="[
            'w-8 h-8 flex items-center justify-center rounded text-sm font-medium transition-colors',
            currentPage === page ? 'bg-gray-900 text-white' : 'hover:bg-gray-50 disabled:opacity-50'
          ]"
        >
          {{ page }}
        </button>
        
        <button 
          @click="currentPage++" 
          :disabled="currentPage === totalPages"
          class="w-8 h-8 flex items-center justify-center rounded border border-gray-200 hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <Icon name="mdi:chevron-right" class="w-5 h-5" />
        </button>
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { MembershipStatus } from '~/types'
import { useAuthStore } from '~/stores/auth'
import { useMemberships } from '~/composables/useMemberships'

const router = useRouter()
const authStore = useAuthStore()
const { getMembershipsByTenant } = useMemberships()

// View state
const isLoading = ref(true)
const apiError = ref('')
const activeTab = ref('all')
const searchQuery = ref('')
const currentPage = ref(1)
const itemsPerPage = 12

// Tabs definition
const tabs = [
  { label: 'Tüm Üyeler', value: 'all' },
  { label: 'Başvurular', value: 'pending' },
  { label: 'Ödeme Bekleyenler', value: 'preapproved' },
  { label: 'Aktif Üyeler', value: 'approved' },
  { label: 'Pasif Üyeler', value: 'passive' }
]

// Status mappings
const statusStyles: Record<number, { label: string, class: string }> = {
  [MembershipStatus.Pending]: { label: 'Beklemede', class: 'bg-amber-100 text-amber-800' },
  [MembershipStatus.PreApproved]: { label: 'Ön Onaylı', class: 'bg-blue-100 text-blue-800' },
  [MembershipStatus.Approved]: { label: 'Aktif Üye', class: 'bg-green-100 text-green-800' },
  [MembershipStatus.Rejected]: { label: 'Reddedildi', class: 'bg-red-100 text-red-800' },
  [MembershipStatus.Suspended]: { label: 'Askıya Alındı', class: 'bg-orange-100 text-orange-800' },
  [MembershipStatus.Cancelled]: { label: 'İptal Edildi', class: 'bg-gray-100 text-gray-800' }
}

// API Data
const members = ref<any[]>([])

const handleTabChange = async (tab: string) => {
  activeTab.value = tab
  await fetchMembers()
}

const fetchMembers = async () => {
  const tenantId = authStore.user?.tenantGroup?.tenantId
  if (!tenantId) {
    apiError.value = 'Geçerli bir kurum bulunamadı.'
    isLoading.value = false
    return
  }
  
  isLoading.value = true
  apiError.value = ''
  
  try {
    let status: number | undefined
    let filterPassiveOnFrontend = false
    
    switch (activeTab.value) {
      case 'pending': status = MembershipStatus.Pending; break
      case 'preapproved': status = MembershipStatus.PreApproved; break
      case 'approved': status = MembershipStatus.Approved; break
      case 'passive': 
        status = undefined
        filterPassiveOnFrontend = true
        break
      default: status = undefined
    }
    
    const response = await getMembershipsByTenant(tenantId, status)
    console.log('API Response:', response)
    
    let fetchedMembers = Array.isArray(response) ? response : (response as any)?.data || response || []
    
    if (filterPassiveOnFrontend) {
      fetchedMembers = fetchedMembers.filter((m: any) => [
        MembershipStatus.Rejected, 
        MembershipStatus.Suspended, 
        MembershipStatus.Cancelled
      ].includes(m.status))
    }
    
    members.value = fetchedMembers
    console.log('Members state:', members.value)
  } catch (err: any) {
    console.error('API Error:', err)
    apiError.value = 'Üye listesi yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.'
  } finally {
    isLoading.value = false
  }
}

onMounted(async () => {
  isLoading.value = true
  
  // If `user` is missing from the store, load it from localStorage
  if (!authStore.user && process.client) {
    const savedUser = localStorage.getItem('user')
    if (savedUser) {
      try {
        const parsedUser = JSON.parse(savedUser)
        authStore.user = parsedUser
        // also set properties normally handled by setUser if needed
        if (parsedUser.permissions) {
          authStore.permissions = parsedUser.permissions.map((p: any) => p.fullPermission)
        }
        if (parsedUser.roles) {
          authStore.roles = parsedUser.roles.map((r: any) => r.name)
        }
      } catch (e) {
        console.error('Error loading user from localStorage', e)
      }
    }
  }

  // Ensure the auth store is initialized (useful for hard refreshes if tokens need processing)
  if (!authStore.user) {
    await authStore.initializeAuth()
  }

  // Redirect to login if user is still null
  if (!authStore.user) {
    router.push('/login')
    return
  }

  await fetchMembers()
})

// Filter actions
const filteredMembers = computed(() => {
  let result = members.value

  // Apply search
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase()
    result = result.filter(m => 
      (m.fullName && m.fullName.toLowerCase().includes(query)) || 
      (m.email && m.email.toLowerCase().includes(query)) ||
      (m.mobilePhone && m.mobilePhone.toLowerCase().includes(query))
    )
  }

  return result
})

// Pagination logic
const totalPages = computed(() => Math.ceil(filteredMembers.value.length / itemsPerPage))

const paginatedMembers = computed(() => {
  // Ensure current page is valid when filtering changes
  if (currentPage.value > totalPages.value) {
    currentPage.value = Math.max(1, totalPages.value)
  }
  
  const start = (currentPage.value - 1) * itemsPerPage
  const end = start + itemsPerPage
  const sliced = filteredMembers.value.slice(start, end)
  // Ensure we don't have an empty page if there are results
  if (sliced.length === 0 && filteredMembers.value.length > 0 && currentPage.value > 1) {
    currentPage.value = 1
    return filteredMembers.value.slice(0, itemsPerPage)
  }
  return sliced
})

const paginationStart = computed(() => {
  if (filteredMembers.value.length === 0) return 0
  return ((currentPage.value - 1) * itemsPerPage) + 1
})

const paginationEnd = computed(() => {
  const end = currentPage.value * itemsPerPage
  return end > filteredMembers.value.length ? filteredMembers.value.length : end
})

// Helper methods
const getInitials = (fullName: string) => {
  if (!fullName) return 'U'
  const parts = fullName.trim().split(' ')
  if (parts.length >= 2) {
    return `${parts[0].charAt(0)}${parts[parts.length - 1].charAt(0)}`.toUpperCase()
  }
  return fullName.charAt(0).toUpperCase()
}

const formatDate = (isoString?: string) => {
  if (!isoString) return ''
  const date = new Date(isoString)
  if (isNaN(date.getTime())) return ''
  return date.toLocaleDateString('tr-TR', { day: 'numeric', month: 'long', year: 'numeric' })
}

const getAvatarColor = (member: any) => {
  const colors = [
    'bg-[#eff6ff] text-[#2563eb]', // blue
    'bg-[#faf5ff] text-[#9333ea]', // purple
    'bg-[#fdf2f8] text-[#db2777]', // pink
    'bg-[#f0fdf4] text-[#16a34a]', // green
    'bg-[#fff7ed] text-[#ea580c]', // orange
    'bg-[#f8fafc] text-[#475569]'  // slate
  ]
  const length = member.fullName ? member.fullName.length : 0
  const index = length % colors.length
  return colors[index]
}

const navigateToProfile = (id: number) => {
  router.push(`/memberships/${id}`)
}

</script>
