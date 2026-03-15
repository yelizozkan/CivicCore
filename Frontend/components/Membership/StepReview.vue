<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Başvuru Özeti</h2>
      <p class="step-description">Bilgilerinizi kontrol edin</p>
    </div>

    <div class="space-y-6">
  
      <!-- Kişisel Bilgiler -->
      <div class="bg-white border border-slate-200 rounded-xl p-5">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-3">
            <div class="w-8 h-8 bg-blue-100 rounded-lg flex items-center justify-center">
              <svg class="w-4 h-4 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Kişisel Bilgiler</h3>
          </div>
          <button @click="goToStep(1)" class="text-sm text-blue-600 hover:text-blue-700 font-medium">
            Düzenle
          </button>
        </div>
        
        <div class="grid grid-cols-2 gap-4">
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Ad Soyad</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.firstName }} {{ formData.lastName }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">T.C. Kimlik No</p>
            <p class="text-sm font-medium text-slate-800">{{ maskIdentity(formData.identityNumber) }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Doğum</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.birthPlace }} - {{ formatDate(formData.birthDate) }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Kan Grubu</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.bloodType }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Anne Adı</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.motherName }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Baba Adı</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.fatherName }}</p>
          </div>
        </div>
      </div>
      
      <!-- İletişim Bilgileri -->
      <div class="bg-white border border-slate-200 rounded-xl p-5">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-3">
            <div class="w-8 h-8 bg-green-100 rounded-lg flex items-center justify-center">
              <svg class="w-4 h-4 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">İletişim Bilgileri</h3>
          </div>
          <button @click="goToStep(2)" class="text-sm text-blue-600 hover:text-blue-700 font-medium">
            Düzenle
          </button>
        </div>
        
        <div class="grid grid-cols-2 gap-4">
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">E-posta</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.email }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Telefon</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.mobilePhone }}</p>
          </div>
          <div class="col-span-2">
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Adres</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.residenceAddress }}</p>
          </div>
        </div>
      </div>
      
      <!-- Meslek Bilgileri -->
      <div class="bg-white border border-slate-200 rounded-xl p-5">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-3">
            <div class="w-8 h-8 bg-amber-100 rounded-lg flex items-center justify-center">
              <svg class="w-4 h-4 text-amber-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 13.255A23.931 23.931 0 0112 15c-3.183 0-6.22-.62-9-1.745M16 6V4a2 2 0 00-2-2h-4a2 2 0 00-2 2v2m4 6h.01M5 20h14a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Meslek Bilgileri</h3>
          </div>
          <button @click="goToStep(3)" class="text-sm text-blue-600 hover:text-blue-700 font-medium">
            Düzenle
          </button>
        </div>
        
        <div class="grid grid-cols-2 gap-4">
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Eğitim</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.educationLevel }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Meslek</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.profession }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Kurum</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.workplaceName || '-' }}</p>
          </div>
          <div>
            <p class="text-xs text-slate-500 uppercase tracking-wide mb-1">Pozisyon</p>
            <p class="text-sm font-medium text-slate-800">{{ formData.workplacePosition || '-' }}</p>
          </div>
        </div>
      </div>
      
      <!-- Motivasyon -->
      <div class="bg-white border border-slate-200 rounded-xl p-5">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-3">
            <div class="w-8 h-8 bg-purple-100 rounded-lg flex items-center justify-center">
              <svg class="w-4 h-4 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Motivasyon</h3>
          </div>
          <button @click="goToStep(4)" class="text-sm text-blue-600 hover:text-blue-700 font-medium">
            Düzenle
          </button>
        </div>
        
        <p class="text-sm text-slate-700 leading-relaxed">{{ formData.motivationText }}</p>
      </div>
      
      <!-- Onaylar -->
      <div class="bg-white border border-slate-200 rounded-xl p-5">
        <div class="flex items-center gap-3 mb-4">
          <div class="w-8 h-8 bg-emerald-100 rounded-lg flex items-center justify-center">
            <svg class="w-4 h-4 text-emerald-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path></svg>
          </div>
          <h3 class="font-semibold text-slate-800">Onaylar</h3>
        </div>
        
        <div class="space-y-2">
          <div class="flex items-center gap-2">
            <svg v-if="formData.kvkkAccepted" class="w-5 h-5 text-emerald-500" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
            <span class="text-sm text-slate-700">KVKK Aydınlatma Metni</span>
          </div>
          <div class="flex items-center gap-2">
            <svg v-if="formData.explicitConsentAccepted" class="w-5 h-5 text-emerald-500" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
            <span class="text-sm text-slate-700">Açık Rıza Metni</span>
          </div>
        </div>
      </div>
      
      <!-- Bilgilendirme -->
      <div class="bg-blue-50 border border-blue-200 rounded-xl p-4">
        <div class="flex gap-3">
          <svg class="w-5 h-5 text-blue-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path></svg>
          <p class="text-sm text-blue-700">
            Başvurunuz yönetim kurulu tarafından değerlendirilecek ve sonuç e-posta ile bildirilecektir.
          </p>
        </div>
      </div>
      
    </div>
  </div>
</template>

<script setup lang="ts">
interface FormData {
  firstName: string
  lastName: string
  identityNumber: string
  birthPlace: string
  birthDate: string
  bloodType: string
  email: string
  mobilePhone: string
  educationLevel: string
  profession: string
  kvkkAccepted: boolean
  explicitConsentAccepted: boolean
  [key: string]: any
}

defineProps<{ formData: FormData }>()
const emit = defineEmits(['edit'])

const maskIdentity = (id: string): string => {
  if (!id || id.length < 6) return id || '-'
  return id.substring(0, 3) + '****' + id.substring(id.length - 3)
}

const formatDate = (date: string): string => {
  if (!date) return '-'
  return new Date(date).toLocaleDateString('tr-TR')
}

const formatEducation = (level: string): string => {
  const map: Record<string, string> = {
    'ilkokul': 'İlkokul', 'ortaokul': 'Ortaokul', 'lise': 'Lise',
    'onlisans': 'Ön Lisans', 'lisans': 'Lisans', 'yukseklisans': 'Yüksek Lisans', 'doktora': 'Doktora'
  }
  return map[level] || level || '-'
}

const goToStep = (step: number) => {
  emit('edit', step)
}

const validate = (): boolean => true
defineExpose({ validate })
</script>

<style scoped>
.step-content {
  animation: fadeIn 0.3s ease;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}

.step-header {
  margin-bottom: 24px;
  text-align: center;
}

.step-title {
  font-size: 28px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 4px;
}

.step-description {
  font-size: 16px;
  color: #64748b;
}

.review-section {
  background-color: white;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
  padding: 16px;
  margin-bottom: 16px;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.05);
}

.section-header {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 12px;
  padding-bottom: 10px;
  border-bottom: 1px solid #f1f5f9;
}

.section-icon {
  font-size: 18px;
}

.section-header h3 {
  flex: 1;
  font-size: 15px;
  font-weight: 600;
  color: #1e293b;
  margin: 0;
}

.edit-btn {
  padding: 6px 12px;
  background-color: white;
  border: 1px solid #e2e8f0;
  border-radius: 6px;
  font-size: 12px;
  font-weight: 500;
  color: #64748b;
  cursor: pointer;
  transition: all 0.2s;
}

.edit-btn:hover {
  background-color: #f8fafc;
  border-color: #3b82f6;
  color: #3b82f6;
}

.info-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}

.info-item {
  background-color: #f8fafc;
  padding: 10px 12px;
  border-radius: 8px;
}

.info-item .label {
  display: block;
  font-size: 11px;
  color: #94a3b8;
  margin-bottom: 2px;
  text-transform: uppercase;
  font-weight: 600;
}

.info-item .value {
  font-size: 13px;
  font-weight: 500;
  color: #334155;
  word-break: break-all;
}

.consent-list {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.consent-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 10px 12px;
  background-color: #f8fafc;
  border-radius: 8px;
  font-size: 13px;
  color: #64748b;
  border: 1px solid transparent;
}

.consent-item.approved {
  background-color: #f0fdf4; /* Green-50 */
  color: #15803d; /* Green-700 */
  border-color: #dcfce7;
}

.consent-item .check {
  font-weight: bold;
}

.note-box {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  padding: 16px;
  background-color: #fffbeb; /* Amber-50 */
  border: 1px solid #fcd34d; /* Amber-300 */
  border-radius: 10px;
  margin-top: 24px;
}

.note-box span:first-child {
  font-size: 16px;
}

.note-box p {
  font-size: 13px;
  color: #b45309; /* Amber-700 */
  margin: 0;
  line-height: 1.5;
}

@media (max-width: 640px) {
  .info-grid {
    grid-template-columns: 1fr;
  }
}
</style>