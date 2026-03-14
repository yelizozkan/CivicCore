<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Meslek Bilgileri</h2>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Eğitim Durumu <span class="required">*</span></label>
        <select
          v-model="localData.educationLevel"
          class="input-field select-field"
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.educationLevel }"
        >
          <option value="" disabled>Seçiniz</option>
          <option value="İlköğretim">İlköğretim</option>
          <option value="Lise">Lise</option>
          <option value="Ön Lisans">Ön Lisans</option>
          <option value="Lisans">Lisans</option>
          <option value="Yüksek Lisans">Yüksek Lisans</option>
          <option value="Doktora">Doktora</option>
        </select>
        <div v-if="errors.educationLevel" class="text-red-500 text-sm mt-1">{{ errors.educationLevel }}</div>
      </div>

      <div class="input-group">
        <label class="input-label">Meslek <span class="required">*</span></label>
        <input
          v-model="localData.profession"
          type="text"
          class="input-field"
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.profession }"
          placeholder="Mesleğiniz"
        />
        <div v-if="errors.profession" class="text-red-500 text-sm mt-1">{{ errors.profession }}</div>
      </div>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Çalıştığı Kurum</label>
        <input v-model="localData.workplaceName" type="text" class="input-field" placeholder="Kurum / Şirket adı (opsiyonel)" />
      </div>

      <div class="input-group">
        <label class="input-label">Görevi / Unvanı</label>
        <input v-model="localData.workplacePosition" type="text" class="input-field" placeholder="Göreviniz (opsiyonel)" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref } from 'vue'

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

const errors = ref<Record<string, string>>({})

const validate = (): boolean => {
  errors.value = {}

  if (!localData.educationLevel) {
    errors.value.educationLevel = 'Eğitim durumu seçiniz'
  }

  if (!localData.profession?.trim()) {
    errors.value.profession = 'Meslek alanı zorunludur'
  }

  return Object.keys(errors.value).length === 0
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
  gap: 20px 24px;
}

@media (max-width: 640px) {
  .form-grid {
    grid-template-columns: 1fr;
  }
}

.input-group {
  display: block;
  margin-bottom: 20px;
}

.input-label {
  display: block;
  font-size: 14px;
  font-weight: 500;
  color: #334155;
  margin-bottom: 4px;
}

.required {
  color: #ef4444;
}

.input-field {
  width: 100%;
  padding: 12px 16px;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
  font-size: 16px;
  color: #1e293b;
  background-color: white;
  transition: all 0.2s ease;
  outline: none;
}

.input-field:focus {
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.input-field::placeholder {
  color: #94a3b8;
}

.select-field {
  appearance: none;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%236b7280' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='M6 8l4 4 4-4'/%3e%3c/svg%3e");
  background-position: right 12px center;
  background-repeat: no-repeat;
  background-size: 20px;
  cursor: pointer;
}

.select-field option {
  background-color: white;
  color: #1e293b;
}

.select-field option:checked {
  background-color: #f1f5f9;
  color: #1e293b;
}
</style>