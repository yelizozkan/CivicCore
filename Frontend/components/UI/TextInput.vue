<template>
  <div class="base-textbox">
    <label v-if="label" class="textbox-label">
      {{ label }}
      <span v-if="required" class="required">*</span>
    </label>
    
    <div class="input-wrapper">
      <span v-if="prependInnerIcon" class="prepend-icon">
        {{ prependInnerIcon }}
      </span>
      
      <input
        :type="type"
        :value="modelValue"
        @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
        :placeholder="placeholder"
        :disabled="disabled"
        :required="required"
        :maxlength="maxlength"
        class="textbox-input"
        :class="{ 'has-icon': prependInnerIcon, 'error': hasError }"
      />
    </div>
    
    <div v-if="hint" class="textbox-hint">{{ hint }}</div>
    <div v-if="errorMessage" class="textbox-error">{{ errorMessage }}</div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'

interface Props {
  modelValue?: string | number
  label?: string
  type?: string
  placeholder?: string
  prependInnerIcon?: string
  rules?: Array<(v: any) => boolean | string>
  required?: boolean
  disabled?: boolean
  hint?: string
  maxlength?: string | number
}

const props = withDefaults(defineProps<Props>(), {
  type: 'text',
  rules: () => []
})

const emit = defineEmits<{
  'update:modelValue': [value: string | number]
}>()

const errorMessage = ref('')
const hasError = computed(() => !!errorMessage.value)

// Validate on value change
watch(() => props.modelValue, (newValue) => {
  if (props.rules && props.rules.length > 0) {
    for (const rule of props.rules) {
      const result = rule(newValue)
      if (result !== true) {
        errorMessage.value = typeof result === 'string' ? result : 'Invalid'
        return
      }
    }
    errorMessage.value = ''
  }
})
</script>

<style scoped>
.base-textbox {
  margin-bottom: 20px;
}

.textbox-label {
  display: block;
  font-size: 14px;
  font-weight: 600;
  color: var(--civic-text-primary, #1a202c);
  margin-bottom: 8px;
}

.required {
  color: #ef4444;
  margin-left: 2px;
}

.input-wrapper {
  position: relative;
  display: flex;
  align-items: center;
}

.prepend-icon {
  position: absolute;
  left: 12px;
  color: var(--civic-text-muted, #a0aec0);
  font-size: 20px;
  pointer-events: none;
}

.textbox-input {
  width: 100%;
  padding: 12px 16px;
  border: 2px solid #e2e8f0;
  border-radius: 12px;
  font-size: 16px;
  color: var(--civic-text-primary, #1a202c);
  background: white;
  transition: all 0.2s ease;
}

.textbox-input.has-icon {
  padding-left: 44px;
}

.textbox-input:focus {
  outline: none;
  border-color: var(--civic-primary, #2563eb);
  box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.1);
}

.textbox-input:disabled {
  background: #f7fafc;
  cursor: not-allowed;
  opacity: 0.6;
}

.textbox-input.error {
  border-color: #ef4444;
}

.textbox-input::placeholder {
  color: var(--civic-text-muted, #a0aec0);
}

.textbox-hint {
  font-size: 12px;
  color: var(--civic-text-secondary, #718096);
  margin-top: 4px;
}

.textbox-error {
  font-size: 12px;
  color: #ef4444;
  margin-top: 4px;
}
</style>