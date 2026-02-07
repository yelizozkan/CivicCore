<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">İletişim Bilgileri</h2>
      <p class="step-description">Size ulaşabileceğimiz bilgileri girin</p>
    </div>

    <div class="input-group">
      <label class="input-label">E-posta Adresi <span class="required">*</span></label>
      <input v-model="localData.email" type="email" class="input-field" placeholder="ornek@email.com" required />
    </div>

    <div class="form-grid">
      <div class="input-group">
        <label class="input-label">Cep Telefonu <span class="required">*</span></label>
        <input v-model="localData.mobilePhone" type="tel" class="input-field" placeholder="05XX XXX XX XX" required />
      </div>

      <div class="input-group">
        <label class="input-label">İş Telefonu</label>
        <input v-model="localData.workPhone" type="tel" class="input-field" placeholder="İş telefonu (opsiyonel)" />
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">İkamet Adresi <span class="required">*</span></label>
      <textarea v-model="localData.residenceAddress" class="input-field textarea-field" placeholder="Ev adresiniz" rows="2" required></textarea>
    </div>

    <div class="input-group">
      <label class="input-label">İş Adresi</label>
      <textarea v-model="localData.workplaceAddress" class="input-field textarea-field" placeholder="İş adresiniz (opsiyonel)" rows="2"></textarea>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch } from 'vue'

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

watch(localData, (newVal) => {
  emit('update:modelValue', { ...props.modelValue, ...newVal })
}, { deep: true })

watch(() => props.modelValue, (newVal) => {
  Object.assign(localData, newVal)
}, { deep: true })

const validate = (): boolean => {
  if (!localData.email?.trim()) return false
  if (!localData.mobilePhone?.trim()) return false
  if (!localData.residenceAddress?.trim()) return false
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

.textarea-field {
  resize: vertical;
  min-height: 60px;
}
</style>