<template>
  <div class="bg-[#f8fafc] min-h-screen">
    <div class="w-full mx-auto p-4 md:p-6 lg:p-8 pt-6">
      <!-- HEADER (Breadcrumb + Actions) -->
      <div class="flex flex-col sm:flex-row sm:items-center justify-between gap-4 mb-6">
        <!-- Breadcrumbs -->
        <div class="flex items-center gap-2 text-[14px]">
          <div class="flex items-center text-blue-600 bg-blue-50 px-2 py-1 rounded-md">
            <v-icon size="16" class="mr-1.5">mdi-account-group-outline</v-icon>
          </div>
          <NuxtLink to="/memberships" class="text-slate-500 hover:text-slate-800 font-medium transition-colors">Üyeler</NuxtLink>
          <span class="text-slate-400 mx-1">›</span>
          <span class="text-slate-800 font-semibold tracking-wide">Profil Görüntüle</span>
        </div>
        
        <!-- Actions -->
        <div class="flex items-center gap-3">
          <button class="px-3.5 py-2 border border-slate-200 bg-white rounded-lg text-[13px] font-semibold text-slate-700 hover:bg-slate-50 hover:shadow-sm transition-all flex items-center gap-2 shadow-[0_1px_2px_rgba(0,0,0,0.02)]">
            <v-icon size="18" class="text-slate-500">mdi-download-outline</v-icon> Rapor İndir
          </button>
          <button class="px-4 py-2 bg-slate-800 text-white rounded-lg text-[13px] font-semibold hover:bg-slate-900 transition-all flex items-center gap-2 shadow-[0_1px_2px_rgba(0,0,0,0.05)]">
            <v-icon size="18" class="text-white/80">mdi-pencil-outline</v-icon> Düzenle
          </button>
        </div>
      </div>

      <!-- Loading State -->
      <div v-if="loading" class="flex items-center justify-center min-h-[400px]">
        <v-progress-circular indeterminate color="primary" size="48"></v-progress-circular>
      </div>

      <!-- MAIN CONTENT LAYOUT -->
      <div v-else-if="member" class="flex flex-col lg:flex-row gap-6">
        
        <aside class="w-full lg:w-80 flex-shrink-0">
          <div class="bg-white rounded-2xl border border-slate-200 p-6 shadow-[0_1px_3px_rgba(0,0,0,0.04)]">
            <!-- Avatar -->
            <div class="flex flex-col items-center mb-6 text-center">
              <div class="w-24 h-24 bg-gradient-to-br from-blue-500 to-indigo-600 rounded-full flex items-center justify-center text-white text-3xl font-bold mb-4 shadow-sm border-4 border-white">
                {{ getInitials(member.fullName) }}
              </div>
              <h2 class="text-xl font-semibold text-slate-800">{{ member.fullName }}</h2>
              <span class="text-sm text-slate-500">Üyelik: {{ formatDate(member.createdDate) }}</span>
              <!-- Status Badge -->
              <span :class="getStatusBadgeClass(member.status)" class="mt-2 px-3 py-1 rounded-full text-xs font-medium">
                {{ getStatusText(member.status) }}
              </span>
            </div>
            
            <!-- İletişim -->
            <div class="space-y-3 mb-6 pb-6 border-b border-slate-100">
              <div class="flex items-center gap-3 text-sm">
                <v-icon class="text-slate-400" size="20">mdi-map-marker-outline</v-icon>
                <span class="text-slate-600 truncate">{{ member.residenceAddress || 'Belirtilmemiş' }}</span>
              </div>
              <div class="flex items-center gap-3 text-sm">
                <v-icon class="text-slate-400" size="20">mdi-phone-outline</v-icon>
                <span class="text-slate-600 truncate">{{ member.mobilePhone || '-' }}</span>
              </div>
              <div class="flex items-center gap-3 text-sm">
                <v-icon class="text-slate-400" size="20">mdi-email-outline</v-icon>
                <span class="text-slate-600 truncate" :title="member.email">{{ member.email || '-' }}</span>
              </div>
            </div>
            
            <!-- Aksiyon Butonları (duruma göre) -->
            <div class="space-y-2">
              <!-- Pending durumunda -->
              <template v-if="member.status === 0">
                <button @click="approveMember" class="w-full px-4 py-2.5 bg-emerald-500 hover:bg-emerald-600 text-white rounded-lg text-sm font-medium transition-colors">
                  Başvuruyu Onayla
                </button>
                <button @click="rejectMember" class="w-full px-4 py-2.5 border border-red-200 text-red-600 hover:bg-red-50 rounded-lg text-sm font-medium transition-colors">
                  Reddet
                </button>
              </template>
              
              <!-- PreApproved durumunda -->
              <template v-else-if="member.status === 1">
                <button @click="recordPayment" class="w-full px-4 py-2.5 bg-blue-500 hover:bg-blue-600 text-white rounded-lg text-sm font-medium transition-colors">
                  Ödeme Kaydet
                </button>
              </template>
              
              <!-- Approved durumunda -->
              <template v-else-if="member.status === 2">
                <button class="w-full px-4 py-2.5 border border-slate-200 text-slate-700 hover:bg-slate-50 rounded-lg text-sm font-medium transition-colors">
                  Mesaj Gönder
                </button>
              </template>
            </div>
            
            <!-- Durum Kartları -->
            <div class="mt-6 space-y-3">
              <!-- When status is Pending -->
              <template v-if="member.status === 0">
                <div class="p-3 bg-amber-50 border border-amber-200 rounded-xl">
                  <p class="text-xs text-amber-600 font-medium">Başvuru İncelemede</p>
                  <p class="text-sm text-amber-800">Başvuru tarihi: {{ formatDate(member.createdDate || member.createdAt) }}</p>
                </div>
              </template>

              <!-- When status is PreApproved -->
              <template v-else-if="member.status === 1">
                <div class="p-3 bg-blue-50 border border-blue-200 rounded-xl">
                  <p class="text-xs text-blue-600 font-medium">Ödeme Bekleniyor</p>
                  <p class="text-sm text-blue-800">Aidat: ₺{{ member.paymentAmount || 'Belirtilmedi' }}</p>
                </div>
              </template>

              <!-- When status is Approved -->
              <template v-else-if="member.status === 2">
                <div class="p-3 bg-emerald-50 border border-emerald-200 rounded-xl">
                  <p class="text-xs text-emerald-600 font-medium">Aktif Üye</p>
                  <p class="text-sm text-emerald-800">Üyelik: {{ formatDate(member.approvedDate || member.createdDate || member.createdAt) }}</p>
                </div>
              </template>

              <!-- When status is Rejected -->
              <template v-else-if="member.status === 3">
                <div class="p-3 bg-red-50 border border-red-200 rounded-xl">
                  <p class="text-xs text-red-600 font-medium">Başvuru Reddedildi</p>
                </div>
              </template>

              <div class="flex items-center justify-between p-3 bg-slate-50 rounded-xl">
                <div>
                  <p class="text-xs text-slate-500">Aidat</p>
                  <p class="text-sm font-semibold text-slate-800">
                    {{ member.status === 0 ? '-' : '₺' + (member.paymentAmount || 0) }}
                  </p>
                </div>
                <span :class="getPaymentStatus(member).class" class="text-xs font-medium">
                  {{ getPaymentStatus(member).text }}
                </span>
              </div>
            </div>
          </div>
        </aside>

        <!-- SAĞ PANEL - Detay Bilgiler -->
        <main class="flex-1 min-w-0 bg-white rounded-2xl border border-slate-200 shadow-[0_1px_3px_rgba(0,0,0,0.04)] overflow-hidden flex flex-col">
          
          <!-- Tabs Navigation -->
          <div class="p-6">
            <section class="mb-8 border-b border-slate-100 pb-8">
              <h3 class="text-lg font-semibold text-slate-800 mb-5">Kişisel Bilgiler</h3>
              <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-x-8 gap-y-6">
                <div>
                  <p class="text-xs text-slate-500 mb-1">Ad</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.firstName || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Soyad</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.lastName || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Doğum Tarihi</p>
                  <p class="text-sm font-medium text-slate-800">{{ formatDate(member.birthDate) }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Doğum Yeri</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.birthPlace || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">TC Kimlik No</p>
                  <p class="text-sm font-medium text-slate-800">{{ maskIdentity(member.identityNumber) }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Kan Grubu</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.bloodType || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Anne Adı</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.motherName || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Baba Adı</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.fatherName || '-' }}</p>
                </div>
              </div>
            </section>
            
            <!-- İletişim Bilgileri -->
            <section class="mb-8 border-b border-slate-100 pb-8">
              <h3 class="text-lg font-semibold text-slate-800 mb-5">İletişim Bilgileri</h3>
              <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-x-8 gap-y-6">
                <div>
                  <p class="text-xs text-slate-500 mb-1">Cep Telefonu</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.mobilePhone || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">İş Telefonu</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.workPhone || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Email</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.email || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Eğitim Durumu</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.educationLevel || '-' }}</p>
                </div>
                <div class="sm:col-span-2 lg:col-span-4">
                  <p class="text-xs text-slate-500 mb-1">Ev Adresi</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.residenceAddress || '-' }}</p>
                </div>
              </div>
            </section>
            
            <!-- Meslek Bilgileri -->
            <section class="mb-8 border-b border-slate-100 pb-8">
              <h3 class="text-lg font-semibold text-slate-800 mb-5">Meslek Bilgileri</h3>
              <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-x-8 gap-y-6">
                <div>
                  <p class="text-xs text-slate-500 mb-1">Meslek</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.profession || '-' }}</p>
                </div>
                <div>
                  <p class="text-xs text-slate-500 mb-1">Pozisyon</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.workplacePosition || '-' }}</p>
                </div>
                <div class="sm:col-span-2 lg:col-span-2">
                  <p class="text-xs text-slate-500 mb-1">Çalıştığı Kurum</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.workplaceName || '-' }}</p>
                </div>
                <div class="sm:col-span-2 lg:col-span-4">
                  <p class="text-xs text-slate-500 mb-1">İş Adresi</p>
                  <p class="text-sm font-medium text-slate-800">{{ member.workplaceAddress || '-' }}</p>
                </div>
              </div>
            </section>
            
            <!-- Motivasyon -->
            <section v-if="member.motivationText">
              <h3 class="text-lg font-semibold text-slate-800 mb-4">Başvuru Motivasyonu</h3>
              <div class="p-4 bg-slate-50 rounded-xl border border-slate-100">
                <p class="text-sm text-slate-700 leading-relaxed">{{ member.motivationText }}</p>
              </div>
            </section>
            
          </div>
        </main>
      </div>
      
      <!-- Error / Not Found State -->
      <div v-else class="flex flex-col items-center justify-center bg-white rounded-2xl border border-slate-200 p-12 mt-6">
        <v-icon size="64" class="text-slate-200 mb-4">mdi-account-off-outline</v-icon>
        <span class="text-[18px] font-bold text-slate-800">Üye Bulunamadı</span>
        <span class="text-slate-500 mt-2 text-center max-w-md">Aradığınız üye profili silinmiş olabilir veya böyle bir kayıt mevcut değil.</span>
        <NuxtLink to="/memberships" class="mt-6 px-5 py-2.5 bg-slate-800 text-white rounded-lg font-semibold hover:bg-slate-900 transition-all text-[14px]">
          Üye Listesine Dön
        </NuxtLink>
      </div>

    </div>

    <!-- Payment Modal -->
    <v-dialog v-model="showPaymentModal" max-width="420" persistent>
      <v-card class="rounded-xl">
        <!-- Header -->
        <div class="flex items-center justify-between px-6 py-4 border-b border-slate-100">
          <div class="flex items-center gap-3">
            <div class="w-10 h-10 bg-blue-100 rounded-lg flex items-center justify-center">
              <v-icon color="blue" size="20">mdi-cash-check</v-icon>
            </div>
            <h3 class="text-[17px] font-semibold text-slate-800">Ödeme Kaydet</h3>
          </div>
          <v-btn icon variant="text" size="small" @click="closePaymentModal">
            <v-icon size="20">mdi-close</v-icon>
          </v-btn>
        </div>
        
        <!-- Content -->
        <div class="px-6 py-5">
          <!-- Ödeme Tutarı -->
          <div class="mb-4">
            <label class="block text-[13px] font-medium text-slate-700 mb-1.5">Ödeme Tutarı</label>
            <div class="relative">
              <span class="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400 text-sm font-medium">₺</span>
              <input 
                v-model="paymentForm.amount"
                type="number"
                class="w-full pl-8 pr-4 py-2.5 border border-slate-200 rounded-lg text-[14px] focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent transition-all"
                placeholder="0.00"
              />
            </div>
          </div>
          
          <!-- Ödeme Tarihi -->
          <div>
            <label class="block text-[13px] font-medium text-slate-700 mb-1.5">Ödeme Tarihi</label>
            <input 
              v-model="paymentForm.date"
              type="date"
              class="w-full px-4 py-2.5 border border-slate-200 rounded-lg text-[14px] focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent transition-all"
            />
          </div>

          <!-- Error Alert -->
          <div v-if="paymentError" class="mt-4 p-3 bg-red-50 text-red-600 text-[13px] font-medium rounded-lg border border-red-100">
            {{ paymentError }}
          </div>
        </div>
        
        <!-- Footer -->
        <div class="flex items-center justify-end gap-3 px-6 py-4 border-t border-slate-100 bg-slate-50 rounded-b-xl">
          <v-btn 
            variant="text" 
            color="grey-darken-1"
            class="text-none font-medium"
            @click="closePaymentModal"
          >
            İptal
          </v-btn>
          <v-btn 
            color="#2563eb"
            class="text-white text-none font-medium px-4"
            elevation="0"
            :loading="paymentLoading"
            @click="submitPayment"
          >
            Kaydet
          </v-btn>
        </div>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup lang="ts">
import { useMemberships } from '~/composables/useMemberships'
import { MembershipStatus, type Membership } from '~/types'

const route = useRoute()
const { getMembershipById } = useMemberships()

const memberId = route.params.id
const loading = ref(true)
const member = ref<any | null>(null)

const fetchMember = async () => {
  const id = Number(route.params.id)
  console.log('Fetching single member, ID:', id)
  
  const response = await getMembershipById(id)
  console.log('Single member response:', response)
  
  // To handle potential API bugs where it incorrectly returns an array instead of singleton
  if (Array.isArray(response)) {
    const found = response.find(m => m.id === id)
    member.value = found || response[0] || null
  } else {
    // Also handle nested 'data' mapping case if `axios/fetch` incorrectly bubbled it
    member.value = (response as any)?.data || response
  }
}

onMounted(async () => {
  loading.value = true
  try {
    if (memberId && !isNaN(Number(memberId))) {
      await fetchMember()
    }
  } catch (err) {
    console.error('Error fetching member details:', err)
  } finally {
    loading.value = false
  }
})

// Actions
const approveMember = () => { console.log('Başvuruyu onayla tıklandı') }
const rejectMember = () => { console.log('Reddet tıklandı') }

// Payment Logic
const showPaymentModal = ref(false)
const paymentLoading = ref(false)
const paymentError = ref('')
const paymentForm = ref({ amount: 0, date: new Date().toISOString().split('T')[0] })

const recordPayment = () => {
  paymentError.value = ''
  paymentForm.value.amount = member.value?.paymentAmount || 0
  paymentForm.value.date = new Date().toISOString().split('T')[0]
  showPaymentModal.value = true
}

const closePaymentModal = () => {
  showPaymentModal.value = false
}

const submitPayment = async () => {
  if (!paymentForm.value.amount || paymentForm.value.amount <= 0) {
    paymentError.value = 'Lütfen geçerli bir ödeme tutarı giriniz.'
    return
  }
  if (!paymentForm.value.date) {
    paymentError.value = 'Lütfen ödeme tarihini seçiniz.'
    return
  }

  paymentLoading.value = true
  paymentError.value = ''

  try {
    // Call the status update endpoint
    // Passing status = 2 (Approved), amount, and date
    // (Ensure your backend accepts these params in UpdateMembershipStatusDto)
    // await updateMembershipStatus({
    //   membershipId: member.value.id,
    //   status: 2,
    //   paymentAmount: paymentForm.value.amount,
    //   paymentReceivedDate: paymentForm.value.date + 'T00:00:00Z'
    // })
    console.log('API call simulation: Payment recorded', {
      membershipId: member.value.id,
      status: 2,
      paymentAmount: paymentForm.value.amount,
      paymentReceivedDate: paymentForm.value.date + 'T00:00:00Z'
    })

    // Update local state temporarily immediately without reloading
    if (member.value) {
      member.value.status = 2 // Approved
      member.value.paymentAmount = paymentForm.value.amount
      member.value.paymentReceivedDate = paymentForm.value.date + 'T00:00:00Z'
      if (!member.value.approvedDate) {
         member.value.approvedDate = new Date().toISOString()
      }
    }
    closePaymentModal()
  } catch (err: any) {
     paymentError.value = err?.response?.data?.message || 'Ödeme kaydedilirken bir hata oluştu.'
     console.error(err)
  } finally {
     paymentLoading.value = false
  }
}


// Determine payment status
const getPaymentStatus = (member: any) => {
  if (!member) return { text: '-', class: 'text-slate-400' }
  // If Approved and there is a payment date → Paid
  if (member.status === 2 && member.paymentReceivedDate) {
    return { text: 'Ödendi', class: 'text-emerald-600' }
  }
  // If PreApproved → Waiting for Payment
  if (member.status === 1) {
    return { text: 'Bekliyor', class: 'text-amber-600' }
  }
  // If Pending → Not yet at the payment stage
  if (member.status === 0) {
    return { text: '-', class: 'text-slate-400' }
  }
  // If Rejected
  if (member.status === 3) {
    return { text: '-', class: 'text-slate-400' }
  }
  return { text: '-', class: 'text-slate-400' }
}

const getInitials = (fullName: string) => {
  if (!fullName) return 'U'
  const names = fullName.trim().split(' ')
  if (names.length >= 2) {
    return (names[0].charAt(0) + names[names.length - 1].charAt(0)).toUpperCase()
  }
  return names[0].charAt(0).toUpperCase()
}

const formatDate = (dateString: string) => {
  if (!dateString) return '-'
  return new Date(dateString).toLocaleDateString('tr-TR', {
    day: 'numeric',
    month: 'long',
    year: 'numeric'
  })
}

const maskIdentity = (id: string) => {
  if (!id || id.length < 6) return id
  return id.slice(0, 3) + '****' + id.slice(-2)
}

const getStatusText = (status: number) => {
  switch (status) {
    case 0: return 'Başvuru Bekliyor'
    case 1: return 'Ödeme Bekliyor'
    case 2: return 'Aktif Üye'
    case 3: return 'Reddedildi'
    case 4: return 'Askıya Alındı'
    case 5: return 'İptal Edildi'
    default: return 'Bilinmiyor'
  }
}

const getStatusBadgeClass = (status: number) => {
  switch (status) {
    case 0: return 'bg-amber-100 text-amber-800'
    case 1: return 'bg-blue-100 text-blue-800'
    case 2: return 'bg-emerald-100 text-emerald-800'
    case 3: return 'bg-red-100 text-red-800'
    case 4: return 'bg-orange-100 text-orange-800'
    case 5: return 'bg-slate-100 text-slate-800'
    default: return 'bg-slate-100 text-slate-800'
  }
}

// Page metadata
definePageMeta({
  title: 'Üye Profili',
  requiresAuth: true
})
</script>

<style scoped>
.animate-fade-in {
  animation: fadeIn 0.3s ease-in-out;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(4px); }
  to { opacity: 1; transform: translateY(0); }
}
</style>
