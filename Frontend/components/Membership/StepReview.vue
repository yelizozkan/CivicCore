<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Başvuru Özeti</h2>
      <p class="step-description">Bilgilerinizi kontrol edin</p>
    </div>

    <!-- Personal Info -->
    <div class="review-section">
      <div class="section-header">
        <span class="section-icon">👤</span>
        <h3>Kişisel Bilgiler</h3>
        <button type="button" class="edit-btn" @click="$emit('edit', 1)">Düzenle</button>
      </div>
      <div class="info-grid">
        <div class="info-item">
          <span class="label">Ad Soyad</span>
          <span class="value">{{ formData.firstName }} {{ formData.lastName }}</span>
        </div>
        <div class="info-item">
          <span class="label">T.C. Kimlik No</span>
          <span class="value">{{ maskId(formData.identityNumber) }}</span>
        </div>
        <div class="info-item">
          <span class="label">Doğum</span>
          <span class="value">{{ formData.birthPlace }} - {{ formatDate(formData.birthDate) }}</span>
        </div>
        <div class="info-item">
          <span class="label">Kan Grubu</span>
          <span class="value">{{ formData.bloodType }}</span>
        </div>
      </div>
    </div>

    <!-- Contact Info -->
    <div class="review-section">
      <div class="section-header">
        <span class="section-icon">📞</span>
        <h3>İletişim</h3>
        <button type="button" class="edit-btn" @click="$emit('edit', 2)">Düzenle</button>
      </div>
      <div class="info-grid">
        <div class="info-item">
          <span class="label">E-posta</span>
          <span class="value">{{ formData.email }}</span>
        </div>
        <div class="info-item">
          <span class="label">Telefon</span>
          <span class="value">{{ formData.mobilePhone }}</span>
        </div>
      </div>
    </div>

    <!-- Professional Info -->
    <div class="review-section">
      <div class="section-header">
        <span class="section-icon">💼</span>
        <h3>Meslek</h3>
        <button type="button" class="edit-btn" @click="$emit('edit', 3)">Düzenle</button>
      </div>
      <div class="info-grid">
        <div class="info-item">
          <span class="label">Eğitim</span>
          <span class="value">{{ formatEducation(formData.educationLevel) }}</span>
        </div>
        <div class="info-item">
          <span class="label">Meslek</span>
          <span class="value">{{ formData.profession }}</span>
        </div>
      </div>
    </div>

    <!-- Consents -->
    <div class="review-section">
      <div class="section-header">
        <span class="section-icon">✅</span>
        <h3>Onaylar</h3>
      </div>
      <div class="consent-list">
        <div class="consent-item" :class="{ approved: formData.kvkkAccepted }">
          <span>{{ formData.kvkkAccepted ? '✓' : '○' }}</span>
          <span>KVKK Aydınlatma Metni</span>
        </div>
        <div class="consent-item" :class="{ approved: formData.explicitConsentAccepted }">
          <span>{{ formData.explicitConsentAccepted ? '✓' : '○' }}</span>
          <span>Açık Rıza Metni</span>
        </div>
      </div>
    </div>

    <div class="note-box">
      <span>ℹ️</span>
      <p>Başvurunuz yönetim kurulu tarafından değerlendirilecek ve sonuç e-posta ile bildirilecektir.</p>
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
defineEmits(['edit'])

const maskId = (id: string): string => {
  if (!id || id.length < 11) return id || '-'
  return id.substring(0, 3) + '*****' + id.substring(8)
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
  margin-bottom: 20px;
  text-align: center;
}

.step-title {
  font-size: 22px;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 4px;
}

.step-description {
  font-size: 14px;
  color: rgba(255, 255, 255, 0.5);
}

.review-section {
  background-color: rgba(255, 255, 255, 0.03);
  border: 1px solid rgba(255, 255, 255, 0.08);
  border-radius: 12px;
  padding: 16px;
  margin-bottom: 12px;
}

.section-header {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 12px;
  padding-bottom: 10px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.05);
}

.section-icon {
  font-size: 18px;
}

.section-header h3 {
  flex: 1;
  font-size: 15px;
  font-weight: 600;
  color: #ffffff;
  margin: 0;
}

.edit-btn {
  padding: 6px 12px;
  background-color: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 6px;
  font-size: 12px;
  color: rgba(255, 255, 255, 0.6);
  cursor: pointer;
  transition: all 0.2s;
}

.edit-btn:hover {
  background-color: rgba(59, 130, 246, 0.2);
  border-color: #3b82f6;
  color: #3b82f6;
}

.info-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;
}

.info-item {
  background-color: rgba(255, 255, 255, 0.02);
  padding: 10px 12px;
  border-radius: 8px;
}

.info-item .label {
  display: block;
  font-size: 11px;
  color: rgba(255, 255, 255, 0.4);
  margin-bottom: 2px;
  text-transform: uppercase;
}

.info-item .value {
  font-size: 13px;
  font-weight: 500;
  color: #ffffff;
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
  background-color: rgba(255, 255, 255, 0.02);
  border-radius: 8px;
  font-size: 13px;
  color: rgba(255, 255, 255, 0.5);
}

.consent-item.approved {
  background-color: rgba(16, 185, 129, 0.1);
  color: #10b981;
}

.note-box {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  padding: 12px 16px;
  background-color: rgba(251, 191, 36, 0.1);
  border: 1px solid rgba(251, 191, 36, 0.2);
  border-radius: 10px;
  margin-top: 16px;
}

.note-box span:first-child {
  font-size: 16px;
}

.note-box p {
  font-size: 13px;
  color: #fbbf24;
  margin: 0;
  line-height: 1.5;
}

@media (max-width: 640px) {
  .info-grid {
    grid-template-columns: 1fr;
  }
}
</style>