<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Motivasyon</h2>
    </div>

    <div class="tips-container compact-tips">
      <div class="tips-content">
        <span class="tips-icon">💡</span>
        <p><strong>İpucu:</strong> Derneğe katılma motivasyonunuzu, ilgi ve deneyim alanlarınızı ve nasıl katkı sunabileceğinizi kısaca paylaşabilirsiniz.</p>
      </div>
    </div>

    <div class="input-group">
      <label class="input-label">Derneğe Katılım Motivasyonunuz <span class="required">*</span></label>
      <textarea 
        v-model="localData.motivationText" 
        class="input-field textarea-field" 
        placeholder="Açıklamanızı buraya yazın..."
        rows="8"
        required
      ></textarea>
      <div class="char-count" :class="{ 'warning': charCount < 50, 'success': charCount >= 50 }">
        {{ charCount }} / 500 karakter
        <span v-if="charCount < 50" class="hint">(en az 50 karakter)</span>
        <span v-else class="hint">✓</span>
      </div>
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
  font-size: 28px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 4px;
}

.step-description {
  font-size: 16px;
  color: #64748b;
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
  border-color: #9ca3af;
  box-shadow: 0 0 0 3px rgba(156, 163, 175, 0.15);
}

.input-field::placeholder {
  color: #94a3b8;
}

.textarea-field {
  resize: vertical;
  min-height: 120px;
  line-height: 1.6;
  font-family: inherit;
}

.char-count {
  margin-top: 8px;
  font-size: 13px;
  color: #64748b;
  text-align: right;
}

.char-count.warning {
  color: #f59e0b;
}

.char-count.success {
  color: #22c55e;
}

.char-count .hint {
  margin-left: 4px;
}

/* Tips Box - Nötr/Gri */
.tips-container.compact-tips {
  margin-top: 0;
  margin-bottom: 24px;
  padding: 14px 18px;
  background-color: #f8fafc;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
}

.tips-content {
  display: flex;
  gap: 12px;
  align-items: flex-start;
}

.tips-icon {
  font-size: 20px;
  line-height: 1;
}

.tips-content p {
  margin: 0;
  font-size: 14px;
  color: #475569;
  line-height: 1.6;
}

.tips-content strong {
  color: #334155;
  font-weight: 600;
}
</style>