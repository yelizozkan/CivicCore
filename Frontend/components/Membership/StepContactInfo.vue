<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">İletişim Bilgileri</h2>
    </div>

    <div class="input-group">
      <label class="input-label">E-posta Adresi <span class="required">*</span></label>
      <input 
        v-model="localData.email" 
        type="email" 
        class="input-field" 
        :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.email }"
        placeholder="ornek@email.com" 
      />
      <div v-if="errors.email" class="text-red-500 text-sm mt-1">{{ errors.email }}</div>
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Cep Telefonu <span class="required">*</span></label>
        <div class="phone-input-wrapper" :class="{ 'has-error': errors.mobilePhone }">
          <span class="phone-prefix" :class="{ 'border-red-500 bg-red-50': errors.mobilePhone }">
            <span class="fi fi-tr"></span>
            +90
          </span>
          <input 
            v-model="displayPhone"
            type="tel" 
            class="input-field phone-field !border-l-0" 
            :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.mobilePhone }"
            placeholder="5XX XXX XX XX" 
            @input="handlePhoneInput"
            maxlength="13"
          />
        </div>
        <div v-if="errors.mobilePhone" class="text-red-500 text-sm mt-1">{{ errors.mobilePhone }}</div>
      </div>

      <div class="input-group">
        <label class="input-label">İş Telefonu</label>
        <input 
          v-model="localData.workPhone" 
          type="tel" 
          class="input-field" 
          :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.workPhone }"
          placeholder="İş telefonu (opsiyonel)" 
        />
        <div v-if="errors.workPhone" class="text-red-500 text-sm mt-1">{{ errors.workPhone }}</div>
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">İkamet Adresi <span class="required">*</span></label>
      <textarea 
        v-model="localData.residenceAddress" 
        class="input-field textarea-field" 
        :class="{ 'border-red-500 focus:border-red-500 focus:ring-red-100': errors.residenceAddress }"
        placeholder="Ev adresiniz" 
        rows="2"
      ></textarea>
      <div v-if="errors.residenceAddress" class="text-red-500 text-sm mt-1">{{ errors.residenceAddress }}</div>
    </div>

    <div class="input-group">
      <label class="input-label">İş Adresi</label>
      <textarea 
        v-model="localData.workplaceAddress" 
        class="input-field textarea-field" 
        placeholder="İş adresiniz (opsiyonel)" 
        rows="2"
      ></textarea>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, ref, onMounted } from 'vue'

interface FormData {
  email: string
  mobilePhone: string
  workPhone: string
  residenceAddress: string
  workplaceAddress: string
  [key: string]: any
}

const props = defineProps<{ modelValue: FormData }>()
const emit = defineEmits(['update:modelValue'])

const localData = reactive({ ...props.modelValue })
const displayPhone = ref('')

// Format initial phone value if exists
onMounted(() => {
  if (localData.mobilePhone) {
    displayPhone.value = formatPhoneNumber(localData.mobilePhone)
  }
})

// Phone formatting logic
const formatPhoneNumber = (value: string) => {
  // Remove all non-digits
  const numbers = value.replace(/\D/g, '')
  
  // Format as 5XX XXX XX XX
  if (numbers.length <= 3) return numbers
  if (numbers.length <= 6) return `${numbers.slice(0, 3)} ${numbers.slice(3)}`
  if (numbers.length <= 8) return `${numbers.slice(0, 3)} ${numbers.slice(3, 6)} ${numbers.slice(6)}`
  return `${numbers.slice(0, 3)} ${numbers.slice(3, 6)} ${numbers.slice(6, 8)} ${numbers.slice(8, 10)}`
}

const handlePhoneInput = (e: Event) => {
  const input = e.target as HTMLInputElement
  let value = input.value.replace(/\D/g, '')
  
  // Ensure it starts with 5 if not empty (optional, depending on strictness)
  // For now just limiting length to 10 digits (without country code)
  if (value.length > 10) value = value.slice(0, 10)
  
  displayPhone.value = formatPhoneNumber(value)
  localData.mobilePhone = value ? `+90${value}` : ''
}

watch(localData, (newVal) => {
  emit('update:modelValue', { ...props.modelValue, ...newVal })
}, { deep: true })

watch(() => props.modelValue, (newVal) => {
  Object.assign(localData, newVal)
  // Reset display phone if model value is cleared or changed externally
  if (newVal.mobilePhone && newVal.mobilePhone !== `+90${displayPhone.value.replace(/\D/g, '')}`) {
      // Extract numbers after +90
      const raw = newVal.mobilePhone.replace(/^\+90/, '')
      displayPhone.value = formatPhoneNumber(raw)
  }
}, { deep: true })

const errors = ref<Record<string, string>>({})

const validate = (): boolean => {
  errors.value = {}
  
  if (!localData.email?.trim()) {
    errors.value.email = 'E-posta adresi zorunludur'
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(localData.email)) {
    errors.value.email = 'Geçerli bir e-posta adresi giriniz'
  }
  
  if (!localData.mobilePhone?.trim()) {
    errors.value.mobilePhone = 'Cep telefonu zorunludur'
  } else if (localData.mobilePhone.length < 13) {
    errors.value.mobilePhone = 'Geçerli bir telefon numarası giriniz'
  }
  
  if (!localData.residenceAddress?.trim()) {
    errors.value.residenceAddress = 'İkamet adresi zorunludur'
  }

  // İş telefonu girilmişse format kontrolü (opsiyonel)
  if (localData.workPhone?.trim() && localData.workPhone.trim().length < 7) {
    errors.value.workPhone = 'Geçerli bir iş telefonu giriniz'
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

.textarea-field {
  resize: vertical;
  min-height: 80px;
  font-family: inherit;
}


/* Phone Input - Professional Style */
.phone-input-wrapper {
  position: relative;
  display: flex;
  align-items: stretch;
}

.phone-prefix {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 14px 14px;
  background: #f8fafc;
  border: 1px solid #d1d5db;
  border-right: none;
  border-radius: 12px 0 0 12px;
  font-size: 15px;
  color: #374151;
  font-weight: 500;
  white-space: nowrap;
}

.phone-prefix .fi {
  font-size: 18px;
  border-radius: 3px;
  box-shadow: 0 1px 2px rgba(0,0,0,0.1);
}

.phone-field {
  border-radius: 0 12px 12px 0;
  padding-left: 14px;
  letter-spacing: 0.5px;
  flex: 1;
}

.phone-field:focus {
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.phone-input-wrapper:focus-within .phone-prefix {
  border-color: #3b82f6;
  background: #eff6ff;
}
</style>