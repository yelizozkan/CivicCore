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

    <v-form ref="form" v-model="isValid">
      <div>
        <label class="field-label">Derneğe Katılım Motivasyonunuz <span class="required">*</span></label>
        <v-textarea
          v-model="formData.motivationText"
          :rules="[rules.required, rules.minLength(50)]"
          variant="outlined"
          density="comfortable"
          placeholder="Açıklamanızı buraya yazın..."
          rows="8"
          hide-details="auto"
          class="mb-2"
          counter
          maxlength="500"
        />
        <div class="char-count" :class="{ 'warning': charCount < 50 && charCount > 0, 'success': charCount >= 50 }">
          {{ charCount }} / 500 karakter
          <span v-if="charCount < 50" class="hint">(en az 50 karakter)</span>
          <span v-else class="hint">✓</span>
        </div>
      </div>
    </v-form>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'

const props = defineProps<{
  formData: any
}>()

const { validationRules: rules } = useValidators()
const form = ref()
const isValid = ref(false)

const charCount = computed(() => props.formData.motivationText?.length || 0)

const validate = async (): Promise<boolean> => {
  if (!form.value) return false
  const { valid } = await form.value.validate()
  return valid
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

.field-label {
  display: block;
  font-size: 14px;
  font-weight: 500;
  color: #334155;
  margin-bottom: 4px;
}

.required {
  color: #ef4444;
}

.char-count {
  margin-top: 6px;
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