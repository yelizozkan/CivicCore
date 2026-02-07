<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Kişisel Bilgiler</h2>
      <p class="step-description">Kimlik bilgilerinizi girin</p>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Ad <span class="required">*</span></label>
        <input v-model="localData.firstName" type="text" class="input-field" placeholder="Adınız" required />
      </div>

      <div class="input-group">
        <label class="input-label">Soyad <span class="required">*</span></label>
        <input v-model="localData.lastName" type="text" class="input-field" placeholder="Soyadınız" required />
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">T.C. Kimlik Numarası <span class="required">*</span></label>
      <input v-model="localData.identityNumber" type="text" class="input-field" placeholder="11 haneli kimlik numarası" maxlength="11" required />
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Anne Adı <span class="required">*</span></label>
        <input v-model="localData.motherName" type="text" class="input-field" placeholder="Anne adı" required />
      </div>

      <div class="input-group">
        <label class="input-label">Baba Adı <span class="required">*</span></label>
        <input v-model="localData.fatherName" type="text" class="input-field" placeholder="Baba adı" required />
      </div>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Doğum Yeri <span class="required">*</span></label>
        <input v-model="localData.birthPlace" type="text" class="input-field" placeholder="Doğum yeri" required />
      </div>

      <div class="input-group">
        <label class="input-label">Doğum Tarihi <span class="required">*</span></label>
        <input v-model="localData.birthDate" type="date" class="input-field" required />
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">Kan Grubu <span class="required">*</span></label>
      <select v-model="localData.bloodType" class="input-field select-field" required>
        <option value="" disabled>Seçiniz</option>
        <option value="A+">A Rh+</option>
        <option value="A-">A Rh-</option>
        <option value="B+">B Rh+</option>
        <option value="B-">B Rh-</option>
        <option value="AB+">AB Rh+</option>
        <option value="AB-">AB Rh-</option>
        <option value="0+">0 Rh+</option>
        <option value="0-">0 Rh-</option>
      </select>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch } from 'vue'

interface FormData {
  firstName: string
  lastName: string
  identityNumber: string
  motherName: string
  fatherName: string
  birthPlace: string
  birthDate: string
  bloodType: string
  [key: string]: any
}

const props = defineProps<{ modelValue: FormData }>()
const emit = defineEmits(['update:modelValue'])

const localData = reactive({ ...props.modelValue })

watch(localData, (newVal) => {
  emit('update:modelValue', { ...props.modelValue, ...newVal })
}, { deep: true })

watch(() => props.modelValue, (newVal) => {
  Object.assign(localData, newVal)
}, { deep: true })

const validate = (): boolean => {
  if (!localData.firstName?.trim()) return false
  if (!localData.lastName?.trim()) return false
  if (!localData.identityNumber?.trim() || localData.identityNumber.length !== 11) return false
  if (!localData.motherName?.trim()) return false
  if (!localData.fatherName?.trim()) return false
  if (!localData.birthPlace?.trim()) return false
  if (!localData.birthDate) return false
  if (!localData.bloodType) return false
  return true
}

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
  font-size: 22px;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 4px;
}

.step-description {
  font-size: 14px;
  color: rgba(255, 255, 255, 0.5);
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}

@media (max-width: 640px) {
  .form-grid {
    grid-template-columns: 1fr;
  }
}

.input-group {
  margin-bottom: 12px;
}

.input-label {
  display: block;
  font-size: 13px;
  font-weight: 500;
  color: rgba(255, 255, 255, 0.7);
  margin-bottom: 6px;
}

.required {
  color: #f87171;
}

.input-field {
  width: 100%;
  padding: 12px 14px;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 10px;
  font-size: 14px;
  color: #ffffff;
  background-color: rgba(255, 255, 255, 0.05);
  transition: all 0.2s ease;
  outline: none;
}

.input-field:focus {
  border-color: #3b82f6;
  background-color: rgba(59, 130, 246, 0.1);
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.input-field::placeholder {
  color: rgba(255, 255, 255, 0.3);
}

.select-field {
  appearance: none;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24' fill='none' stroke='rgba(255,255,255,0.5)' stroke-width='2'%3E%3Cpolyline points='6 9 12 15 18 9'%3E%3C/polyline%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right 12px center;
  background-size: 16px;
  padding-right: 40px;
  cursor: pointer;
}

.select-field option {
  background-color: #1e293b;
  color: #ffffff;
}
</style>