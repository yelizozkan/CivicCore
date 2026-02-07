<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Motivasyon</h2>
      <p class="step-description">Derneğimize katılmak isteme nedeninizi paylaşın</p>
    </div>

    <div class="input-group">
      <label class="input-label">Derneğe Katılım Motivasyonunuz <span class="required">*</span></label>
      <textarea 
        v-model="localData.motivationText" 
        class="input-field textarea-field" 
        placeholder="Derneğimize neden katılmak istediğinizi, beklentilerinizi ve katkıda bulunmak istediğiniz alanları kısaca açıklayın..."
        rows="6"
        required
      ></textarea>
      <div class="char-count" :class="{ 'warning': charCount < 50 }">
        {{ charCount }} / 500 karakter
        <span v-if="charCount < 50" class="hint">(en az 50 karakter)</span>
      </div>
    </div>

    <div class="tips-box">
      <div class="tips-header">
        <span class="tips-icon">💡</span>
        <span>İpuçları</span>
      </div>
      <ul class="tips-list">
        <li>Derneğimizi nasıl tanıdığınızı belirtebilirsiniz</li>
        <li>Hangi alanlarda katkıda bulunmak istediğinizi yazabilirsiniz</li>
        <li>Beklentilerinizi ve hedeflerinizi paylaşabilirsiniz</li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch, computed } from 'vue'

interface FormData {
  motivationText: string
  [key: string]: any
}

const props = defineProps<{ modelValue: FormData }>()
const emit = defineEmits(['update:modelValue'])

const localData = reactive({ ...props.modelValue })

const charCount = computed(() => localData.motivationText?.length || 0)

watch(localData, (newVal) => {
  emit('update:modelValue', { ...props.modelValue, ...newVal })
}, { deep: true })

watch(() => props.modelValue, (newVal) => {
  Object.assign(localData, newVal)
}, { deep: true })

const validate = (): boolean => {
  if (!localData.motivationText?.trim() || localData.motivationText.length < 50) return false
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

.input-group {
  margin-bottom: 16px;
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
  min-height: 120px;
  line-height: 1.6;
}

.char-count {
  margin-top: 8px;
  font-size: 12px;
  color: rgba(255, 255, 255, 0.4);
  text-align: right;
}

.char-count.warning {
  color: #fbbf24;
}

.char-count .hint {
  margin-left: 4px;
}

.tips-box {
  background-color: rgba(59, 130, 246, 0.1);
  border: 1px solid rgba(59, 130, 246, 0.2);
  border-radius: 12px;
  padding: 16px;
}

.tips-header {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 14px;
  font-weight: 600;
  color: #3b82f6;
  margin-bottom: 12px;
}

.tips-icon {
  font-size: 18px;
}

.tips-list {
  margin: 0;
  padding-left: 20px;
}

.tips-list li {
  font-size: 13px;
  color: rgba(255, 255, 255, 0.6);
  margin-bottom: 6px;
  line-height: 1.5;
}

.tips-list li:last-child {
  margin-bottom: 0;
}
</style>