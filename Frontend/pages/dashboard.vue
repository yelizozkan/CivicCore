<template>
  <!-- Main White Card Container -->
  <div class="bg-white rounded-[20px] shadow-[0_1px_3px_rgba(0,0,0,0.08)] p-6 lg:p-8 lg:px-10 w-full min-h-[calc(100vh-48px)] border border-gray-100 flex flex-col mx-auto">
    <div class="flex-1 w-full max-w-[1600px] mx-auto">
      
      <!-- Loading State -->
      <div v-if="loading" class="d-flex justify-center align-center" style="height: 400px;">
        <v-progress-circular indeterminate color="primary" size="64"></v-progress-circular>
      </div>

      <div v-else class="h-full">
        <!-- Identity / Header Section (Minimalist PrimeStay Style) -->
        <div class="flex justify-between items-center mb-10 pt-4 px-2" v-if="authStore.user?.tenantGroup">
          
          <div class="flex items-center">
            
            <div>
              <h1 class="text-[32px] font-bold text-gray-900 leading-tight">
                {{ authStore.user.tenantGroup.name || 'Dashboard' }}
              </h1>
              
              <div class="flex items-center gap-6 mt-4 border-b border-gray-200 pb-0">
                <div class="pb-3 border-b-2 border-green-600 text-green-700 font-semibold text-sm cursor-pointer">
                  Genel Bakış
                </div>
                <div class="pb-3 text-gray-500 hover:text-gray-700 font-medium text-sm cursor-pointer transition-colors">
                  Üyeler
                </div>
                 <div class="pb-3 text-gray-500 hover:text-gray-700 font-medium text-sm cursor-pointer transition-colors">
                  Finansal
                </div>
                 <div class="pb-3 text-gray-500 hover:text-gray-700 font-medium text-sm cursor-pointer transition-colors">
                  Sistem Ayarları
                </div>
              </div>
            </div>
          </div>
          
          <div class="flex items-center gap-4 self-start mt-2">
             <LanguageSelector />
             
             <button class="flex items-center gap-2 px-4 py-2 rounded-lg bg-white border border-gray-200 shadow-sm hover:bg-gray-50 transition-colors text-sm font-semibold text-gray-700">
               <v-icon size="18" class="text-gray-500">mdi-export-variant</v-icon>
               İçe Aktar
             </button>
          </div>
          
        </div>
        
        <!-- Quick stats total header -->
        <div class="flex items-center justify-between mt-6 px-2 mb-6">
          <div class="flex items-center gap-3">
            <h2 class="text-lg font-bold text-gray-900">Platform Verileri</h2>
            <span class="text-sm text-gray-500 font-medium">Toplam {{ stats.totalMembers }} kayıt</span>
          </div>
        </div>

        <!-- Stats Overview -->
        <v-row class="mb-6 mx-0 group-stats-row">
          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-blue-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#3b82f6" size="24">mdi-account-multiple</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.totalMembers }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Aktif Üyeler</div>
              </v-card-text>
            </v-card>
          </v-col>
          
          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-amber-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#f59e0b" size="24">mdi-clock-outline</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.pendingCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Bekleyen İnceleme</div>
              </v-card-text>
            </v-card>
          </v-col>

          <v-col cols="12" sm="6" md="3" class="px-2">
             <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-purple-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#8b5cf6" size="24">mdi-cash-clock</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.waitingPaymentCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Ödeme Bekliyor</div>
              </v-card-text>
            </v-card>
          </v-col>

          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-red-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#ef4444" size="24">mdi-alert-circle-outline</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.overdueCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Gecikmiş Ödemeler</div>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>

        <!-- Action Required Section -->
        <div class="mb-4" v-if="stats.pendingCount > 0 || stats.waitingPaymentCount > 0 || stats.overdueCount > 0">
          <h2 class="text-[16px] font-bold text-gray-900 mb-3 px-1">Gereken İşlemler</h2>
          <v-row>
            <v-col cols="12" md="4" v-if="stats.pendingCount > 0" class="px-2">
              <v-alert
                color="warning"
                variant="tonal"
                class="rounded-lg shadow-sm"
                icon="mdi-account-clock"
                border="start"
                closable
                style="font-size: 14px;"
              >
                <template v-slot:title>
                  <span class="text-[15px] font-bold">{{ stats.pendingCount }} Yeni Başvuru</span>
                </template>
                İncelemenizi bekleyen yeni üyelik başvuruları var.
                <div class="mt-3">
                  <v-btn size="small" variant="flat" color="warning" class="text-white text-xs font-semibold rounded-md text-none px-4">Şimdi İncele</v-btn>
                </div>
              </v-alert>
            </v-col>

            <v-col cols="12" md="4" v-if="stats.waitingPaymentCount > 0" class="px-2">
              <v-alert
                color="info"
                variant="tonal"
                class="rounded-lg shadow-sm"
                icon="mdi-cash-clock"
                border="start"
                closable
                style="font-size: 14px;"
              >
                <template v-slot:title>
                   <span class="text-[15px] font-bold">{{ stats.waitingPaymentCount }} Ödeme Bekliyor</span>
                </template>
                Ön onaylı ancak henüz aidat ödememiş üyeler.
                <div class="mt-3">
                  <v-btn size="small" variant="flat" color="info" class="text-white text-xs font-semibold rounded-md text-none px-4">Listeyi Gör</v-btn>
                </div>
              </v-alert>
            </v-col>

            <v-col cols="12" md="4" v-if="stats.overdueCount > 0" class="px-2">
              <v-alert
                color="error"
                variant="tonal"
                class="rounded-lg shadow-sm"
                icon="mdi-alert-circle"
                border="start"
                closable
                style="font-size: 14px;"
              >
                <template v-slot:title>
                  <span class="text-[15px] font-bold">{{ stats.overdueCount }} Gecikmiş Ödeme</span>
                </template>
                30 günden fazla gecikmiş ödemesi olan üyeler.
                <div class="mt-3">
                  <v-btn size="small" variant="flat" color="error" class="text-white text-xs font-semibold rounded-md text-none px-4">İşlem Yap</v-btn>
                </div>
              </v-alert>
            </v-col>
          </v-row>
        </div>

        <!-- Recent Applications & Quick Actions -->
        <v-row class="mx-0">
          <v-col cols="12" lg="8" class="px-2">
            <v-card class="rounded-xl border border-gray-200 bg-white" elevation="0" style="box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-title class="flex justify-between items-center py-4 px-6 border-b border-gray-100">
                <span class="text-[18px] font-semibold text-slate-800">Son Başvurular</span>
                <button class="text-[13px] font-medium text-blue-500 hover:text-blue-600 uppercase tracking-wider transition-colors">TÜMÜNÜ GÖR →</button>
              </v-card-title>
              
              <v-data-table
                :headers="headers"
                :items="recentApplications"
                :loading="loading"
                hover
                class="custom-primestay-table"
                :items-per-page="5"
                items-per-page-text="Sayfa başına kayıt:"
                page-text="{0}-{1} / {2}"
              >
                <template v-slot:item.status="{ item }">
                  <div class="inline-flex items-center px-2.5 py-1 rounded-full text-[13px] font-medium" :class="getBadgeClass(item.status)">
                    <span class="mr-1.5 text-[10px]">●</span>
                    {{ getStatusText(item.status) }}
                  </div>
                </template>

                <template v-slot:item.createdAt="{ item }">
                  <span class="text-[14px] text-[#374151]">{{ new Date(item.createdAt).toLocaleDateString('tr-TR', { day: 'numeric', month: 'short', year: 'numeric' }) }}</span>
                </template>

                 <template v-slot:item.firstName="{ item }">
                  <div class="flex items-center gap-3">
                    <div class="w-10 h-10 rounded-full flex items-center justify-center text-sm font-semibold bg-[#eff6ff] text-[#2563eb]">
                      {{ item.firstName?.charAt(0) || 'U' }}{{ item.lastName?.charAt(0) || '' }}
                    </div>
                    <div class="flex flex-col">
                      <span class="text-[14px] font-semibold text-[#1e293b]">{{ item.firstName }} {{ item.lastName }}</span>
                      <span class="text-[13px] text-[#64748b]">{{ item.email || '-' }}</span>
                    </div>
                  </div>
                </template>

                <template v-slot:item.phone="{ item }">
                  <span class="text-[14px] text-[#374151]">{{ item.mobilePhone || item.phone || '-' }}</span>
                </template>

                 <template v-slot:item.actions="{ item }">
                  <div class="flex items-center justify-end gap-2">
                    <button class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                      <v-icon size="18">mdi-download-outline</v-icon>
                    </button>
                    <button class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                      <v-icon size="18">mdi-eye-outline</v-icon>
                    </button>
                  </div>
                </template>
                
                <template v-slot:no-data>
                  <div class="py-12 flex flex-col items-center justify-center text-center">
                    <div class="w-16 h-16 bg-gray-50 flex items-center justify-center mb-4 border border-gray-100" style="border-radius: 12px;">
                      <v-icon size="36" color="#cbd5e1">mdi-inbox-outline</v-icon>
                    </div>
                    <span class="text-[15px] font-semibold text-slate-800 mb-1">Bekleyen başvuru bulunamadı</span>
                    <span class="text-[13px] text-[#94a3b8] font-medium">Gelen yeni kayıtlar burada listelenecektir.</span>
                  </div>
                </template>
              </v-data-table>
            </v-card>
          </v-col>

          <v-col cols="12" lg="4" class="px-2">
             <v-card class="rounded-xl mb-6 border border-gray-200 bg-white" elevation="0" style="padding: 20px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <h3 class="text-[16px] font-semibold text-slate-800 mb-4 px-1">Hızlı İşlemler</h3>
              
              <div class="flex flex-col gap-2 mb-6">
                
                <button @click="copyRegistrationLink" class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-blue-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-blue-500 transition-colors">mdi-account-plus-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Üye Kayıt Linki</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-blue-500">mdi-content-copy</v-icon>
                </button>

                <button class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-amber-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-amber-500 transition-colors">mdi-file-export-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Üye Listesini Dışa Aktar</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-amber-500">mdi-chevron-right</v-icon>
                </button>

                <button class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-purple-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-purple-500 transition-colors">mdi-cog-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Grup Ayarları</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-purple-500">mdi-chevron-right</v-icon>
                </button>

              </div>
              
              <!-- Helper/Tip Card -->
              <div class="bg-[#f8fafc] rounded-xl border border-dashed border-gray-300 p-4">
                <div class="flex items-start gap-3">
                  <div class="shrink-0 mt-0.5">
                    <v-icon size="20" color="#64748b">mdi-lightbulb-on-outline</v-icon>
                  </div>
                  <div>
                    <span class="text-[12px] font-bold text-slate-700 uppercase tracking-widest block mb-1">İpucu</span>
                    <p class="text-[13px] text-slate-600 font-medium leading-tight m-0">
                      Üye listesinde birden fazla üyeyi seçerek toplu onay verebilirsiniz.
                    </p>
                  </div>
                </div>
              </div>
            </v-card>
          </v-col>
        </v-row>

        <!-- TenantGroup Details Dialog Component -->
        <TenantGroupDetailDialog
          v-model="showGroupDetails"
          :tenant-group="authStore.user?.tenantGroup"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '~/stores/auth'
import { useMemberships } from '~/composables/useMemberships'
import TenantGroupDetailDialog from '~/components/TenantGroupDetailDialog.vue'
import { MembershipStatus, type Membership } from '~/types'

const authStore = useAuthStore()
const { getMembershipsByGroup, getMembershipsWaitingPayment, getMembershipsOverduePayment } = useMemberships()

const showGroupDetails = ref(false)
const loading = ref(true)
const showSnackbar = ref(false)
const snackbarMessage = ref('')

// Data
const recentApplications = ref<Membership[]>([])
const stats = reactive({
  totalMembers: 0,
  pendingCount: 0,
  waitingPaymentCount: 0,
  overdueCount: 0
})

const headers: any[] = [
  { title: 'BAŞVURU SAHİBİ', key: 'firstName', align: 'start', sortable: false },
  { title: 'TELEFON', key: 'phone', align: 'start', sortable: false },
  { title: 'BAŞVURU TARİHİ', key: 'createdAt', align: 'start', sortable: false },
  { title: 'DURUM', key: 'status', align: 'start', sortable: false },
  { title: 'İŞLEM', key: 'actions', align: 'end', sortable: false },
]

// Custom Badge Styling Logic for Target Component Design
const getBadgeClass = (status: MembershipStatus) => {
  switch (status) {
    case MembershipStatus.Approved: 
      return 'bg-[#dcfce7] text-[#166534]'
    case MembershipStatus.Pending: 
      return 'bg-[#fef3c7] text-[#92400e]'
    case MembershipStatus.Rejected: 
      return 'bg-[#fee2e2] text-[#991b1b]'
    case MembershipStatus.PreApproved: 
      return 'bg-blue-100 text-blue-800'
    case MembershipStatus.Suspended: 
      return 'bg-gray-100 text-gray-800'
    default: 
      return 'bg-gray-100 text-gray-800'
  }
}

const getStatusText = (status: MembershipStatus) => {
   switch (status) {
    case MembershipStatus.Pending: return 'Beklemede'
    case MembershipStatus.PreApproved: return 'Ön Onaylı'
    case MembershipStatus.Approved: return 'Onaylı'
    case MembershipStatus.Rejected: return 'Reddedildi'
    case MembershipStatus.Suspended: return 'Askıda'
    case MembershipStatus.Cancelled: return 'İptal'
    default: return 'Bilinmiyor'
  }
}

const copyRegistrationLink = async () => {
  const slug = authStore.user?.tenantGroup?.slug
  if (!slug) {
    snackbarMessage.value = 'Grup bilgisi bulunamadı.'
    showSnackbar.value = true
    return
  }
  
  const link = `${window.location.origin}/register/${slug}`
  
  try {
    await navigator.clipboard.writeText(link)
    // Show success message
    snackbarMessage.value = 'Link kopyalandı!'
    showSnackbar.value = true
  } catch (err) {
    console.error('Copy failed:', err)
    snackbarMessage.value = 'Link kopyalanamadı.'
    showSnackbar.value = true
  }
}

// Fetch Data
const fetchDashboardData = async () => {
  if (!authStore.user?.tenantGroupId) return

  loading.value = true
  try {
    const groupId = authStore.user.tenantGroupId

    // Parallel API calls with safe handling
    const [allMembersRes, pendingMembersRes, waitingPaymentRes, overduePaymentsRes] = await Promise.all([
      getMembershipsByGroup(groupId).catch(e => { console.error('Error fetching members:', e); return [] }), 
      getMembershipsByGroup(groupId, MembershipStatus.Pending).catch(e => { console.error('Error fetching pending:', e); return [] }),
      getMembershipsWaitingPayment().catch(e => { console.error('Error fetching waiting payment:', e); return [] }),
      getMembershipsOverduePayment().catch(e => { console.error('Error fetching overdue:', e); return [] })
    ])

    // Safe Data Handling
    // If API returns wrap in object with data or array directly
    // Using simple Array.isArray check as existing code suggests
    const allMembers = Array.isArray(allMembersRes) ? allMembersRes : (allMembersRes as any)?.data || []
    const pendingMembers = Array.isArray(pendingMembersRes) ? pendingMembersRes : (pendingMembersRes as any)?.data || []
    const waitingPayment = Array.isArray(waitingPaymentRes) ? waitingPaymentRes : (waitingPaymentRes as any)?.data || []
    const overduePayments = Array.isArray(overduePaymentsRes) ? overduePaymentsRes : (overduePaymentsRes as any)?.data || []

    // Update Stats
    stats.totalMembers = allMembers.filter((m: Membership) => m.status === MembershipStatus.Approved).length
    stats.pendingCount = pendingMembers.length
    stats.waitingPaymentCount = waitingPayment.length
    stats.overdueCount = overduePayments.length

    // Update Recent Applications (Pending ones, limit 5)
    // Temporary mock data for testing table styles
    recentApplications.value = [
      {
        id: 1,
        firstName: 'Ali',
        lastName: 'Yılmaz',
        email: 'ali.yilmaz@example.com',
        mobilePhone: '+90 532 123 45 67',
        status: MembershipStatus.Pending,
        createdAt: new Date().toISOString()
      },
      {
        id: 2,
        firstName: 'Ayşe',
        lastName: 'Demir',
        email: 'ayse.demir@example.com',
        mobilePhone: '+90 533 987 65 43',
        status: MembershipStatus.Approved,
        createdAt: new Date().toISOString()
      },
      {
        id: 3,
        firstName: 'Mehmet',
        lastName: 'Kaya',
        email: 'mehmet.kaya@example.com',
        mobilePhone: '+90 534 555 44 33',
        status: MembershipStatus.Rejected,
        createdAt: new Date().toISOString()
      }
    ] as any[]

  } catch (error) {
    console.error('Error fetching dashboard data:', error)
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchDashboardData()
})

// Page metadata
definePageMeta({
  title: 'Dashboard',
  requiresAuth: true
})
</script>

<style scoped>
.container {
  max-width: 1200px;
}

/* Custom Vuetify Table Override to match PrimeStay Minimal Rules */
:deep(.custom-primestay-table) {
  background: white;
}
:deep(.custom-primestay-table .v-table__wrapper > table > thead > tr > th) {
  background-color: #f8fafc !important;
  color: #64748b !important;
  font-size: 12px !important;
  font-weight: 600 !important;
  text-transform: uppercase !important;
  letter-spacing: 0.5px !important;
  padding: 12px 16px !important;
  border-bottom: 1px solid #f1f5f9 !important;
}
:deep(.custom-primestay-table .v-table__wrapper > table > tbody > tr > td) {
  padding: 16px !important;
  border-bottom: 1px solid #f1f5f9 !important;
}
:deep(.custom-primestay-table .v-table__wrapper > table > tbody > tr:hover > td) {
  background-color: #f8fafc !important;
}
</style>
