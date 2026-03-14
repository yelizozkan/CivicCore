<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Kişisel Bilgiler</h2>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Ad <span class="required">*</span></label>
        <input 
          v-model="localData.firstName" 
          type="text" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.firstName }"
          placeholder="Adınız" 
        />
        <div v-if="errors.firstName" class="text-red-500 text-sm mt-1">{{ errors.firstName }}</div>
      </div>

      <div class="input-group">
        <label class="input-label">Soyad <span class="required">*</span></label>
        <input 
          v-model="localData.lastName" 
          type="text" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.lastName }"
          placeholder="Soyadınız" 
        />
        <div v-if="errors.lastName" class="text-red-500 text-sm mt-1">{{ errors.lastName }}</div>
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">T.C. Kimlik Numarası <span class="required">*</span></label>
      <input 
        v-model="localData.identityNumber" 
        type="text" 
        class="input-field" 
        :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.identityNumber }"
        placeholder="11 haneli kimlik numarası" 
        maxlength="11" 
      />
      <div v-if="errors.identityNumber" class="text-red-500 text-sm mt-1">{{ errors.identityNumber }}</div>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Anne Adı <span class="required">*</span></label>
        <input 
          v-model="localData.motherName" 
          type="text" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.motherName }"
          placeholder="Anne adı" 
        />
        <div v-if="errors.motherName" class="text-red-500 text-sm mt-1">{{ errors.motherName }}</div>
      </div>

      <div class="input-group">
        <label class="input-label">Baba Adı <span class="required">*</span></label>
        <input 
          v-model="localData.fatherName" 
          type="text" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.fatherName }"
          placeholder="Baba adı" 
        />
        <div v-if="errors.fatherName" class="text-red-500 text-sm mt-1">{{ errors.fatherName }}</div>
      </div>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Doğum Yeri <span class="required">*</span></label>
        <input 
          v-model="localData.birthPlace" 
          type="text" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.birthPlace }"
          placeholder="Doğum yeri" 
        />
        <div v-if="errors.birthPlace" class="text-red-500 text-sm mt-1">{{ errors.birthPlace }}</div>
      </div>

      <div class="input-group">
        <label class="input-label">Doğum Tarihi <span class="required">*</span></label>
        <input 
          v-model="localData.birthDate" 
          type="date" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.birthDate }"
        />
        <div v-if="errors.birthDate" class="text-red-500 text-sm mt-1">{{ errors.birthDate }}</div>
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">Kan Grubu <span class="required">*</span></label>
      <select 
        v-model="localData.bloodType" 
        class="input-field select-field focus:border-slate-400 focus:ring-2 focus:ring-slate-100 transition-all appearance-none cursor-pointer" 
        :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.bloodType }"
      >
        <option value="" disabled class="text-slate-400">Kan grubunuzu seçiniz</option>
        <option value="A Rh+">A Rh+</option>
        <option value="A Rh-">A Rh-</option>
        <option value="B Rh+">B Rh+</option>
        <option value="B Rh-">B Rh-</option>
        <option value="AB Rh+">AB Rh+</option>
        <option value="AB Rh-">AB Rh-</option>
        <option value="0 Rh+">0 Rh+</option>
        <option value="0 Rh-">0 Rh-</option>
      </select>
      <div v-if="errors.bloodType" class="text-red-500 text-sm mt-1">{{ errors.bloodType }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref } from 'vue'

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

const errors = ref<Record<string, string>>({})

const validate = (): boolean => {
  errors.value = {}
  
  if (!localData.firstName?.trim()) {
    errors.value.firstName = 'Ad alanı zorunludur'
  }
  if (!localData.lastName?.trim()) {
    errors.value.lastName = 'Soyad alanı zorunludur'
  }
  if (!localData.identityNumber?.trim()) {
    errors.value.identityNumber = 'TC Kimlik No zorunludur'
  } else if (localData.identityNumber.length !== 11) {
    errors.value.identityNumber = 'TC Kimlik No 11 haneli olmalıdır'
  }
  if (!localData.motherName?.trim()) {
    errors.value.motherName = 'Anne adı zorunludur'
  }
  if (!localData.fatherName?.trim()) {
    errors.value.fatherName = 'Baba adı zorunludur'
  }
  if (!localData.birthPlace?.trim()) {
    errors.value.birthPlace = 'Doğum yeri zorunludur'
  }
  if (!localData.birthDate) {
    errors.value.birthDate = 'Doğum tarihi zorunludur'
  }
  if (!localData.bloodType) {
    errors.value.bloodType = 'Kan grubu seçiniz'
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