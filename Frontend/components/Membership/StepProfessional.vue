<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Meslek Bilgileri</h2>
    </div>

    <div class="input-group">
      <label class="input-label">Eğitim Durumu <span class="required">*</span></label>
      <select v-model="localData.educationLevel" class="input-field select-field" required>
        <option value="" disabled>Seçiniz</option>
        <option value="ilkokul">İlkokul</option>
        <option value="ortaokul">Ortaokul</option>
        <option value="lise">Lise</option>
        <option value="onlisans">Ön Lisans</option>
        <option value="lisans">Lisans</option>
        <option value="yukseklisans">Yüksek Lisans</option>
        <option value="doktora">Doktora</option>
      </select>
    </div>

    <div class="input-group">
      <label class="input-label">Meslek <span class="required">*</span></label>
      <input v-model="localData.profession" type="text" class="input-field" placeholder="Mesleğiniz" required />
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Çalıştığınız Kurum</label>
        <input v-model="localData.workplaceName" type="text" class="input-field" placeholder="Kurum adı (opsiyonel)" />
      </div>

      <div class="input-group">
        <label class="input-label">Pozisyon / Görev</label>
        <input v-model="localData.workplacePosition" type="text" class="input-field" placeholder="Pozisyonunuz (opsiyonel)" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch } from 'vue'

interface FormData {
  educationLevel: string
  profession: string
  workplaceName: string
  workplacePosition: string
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
  if (!localData.educationLevel) return false
  if (!localData.profession?.trim()) return false
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
  font-size: 28px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 4px;
}

.step-description {
  font-size: 16px;
  color: #64748b;
}

.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
}

@media (max-width: 640px) {
  .form-grid {
    grid-template-columns: 1fr;
  }
}

.input-group {
  margin-bottom: 20px;
}

.input-label {
  display: block;
  font-size: 15px;
  font-weight: 600;
  color: #334155;
  margin-bottom: 6px;
}

.required {
  color: #ef4444;
}

.input-field {
  width: 100%;
  padding: 14px 16px;
  border: 1px solid #d1d5db;
  border-radius: 12px;
  font-size: 16px;
  color: #1e293b;
  background-color: white;
  transition: all 0.2s ease;
  outline: none;
}

.input-field:focus {
  border-color: #22c55e;
  box-shadow: 0 0 0 3px rgba(34, 197, 94, 0.12);
}

.input-field::placeholder {
  color: #94a3b8;
}

.select-field {
  appearance: none;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24' stroke='%2364748b'%3E%3Cpath stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M19 9l-7 7-7-7'%3E%3C/path%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right 12px center;
  background-size: 16px;
  padding-right: 40px;
  cursor: pointer;
}

.select-field option {
  background-color: white;
  color: #1e293b;
}

.select-field option:checked,
.select-field option:hover {
  background-color: #dcfce7;
  color: #166534;
}
</style>