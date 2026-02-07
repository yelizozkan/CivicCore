<template>
  <button
    v-bind="$attrs"
    class="base-button"
    :class="[
      variant ? `base-button--${variant}` : '',
      size ? `base-button--${size}` : '',
      { 'base-button--block': block },
      { 'base-button--disabled': disabled }
    ]"
    :disabled="disabled"
    @click="$emit('click', $event)"
  >
    <slot name="prepend"></slot>
    <span class="base-button__content">
      <slot></slot>
    </span>
    <slot name="append"></slot>
  </button>
</template>

<script setup lang="ts">
defineProps({
  variant: {
    type: String,
    default: 'primary',
    validator: (value: string) => ['primary', 'secondary', 'text', 'outlined'].includes(value)
  },
  size: {
    type: String,
    default: 'medium',
    validator: (value: string) => ['small', 'medium', 'large'].includes(value)
  },
  block: {
    type: Boolean,
    default: false
  },
  disabled: {
    type: Boolean,
    default: false
  }
})

defineEmits(['click'])
</script>

<style scoped>
.base-button {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  border-radius: 12px;
  font-weight: 600;
  transition: all 0.2s ease;
  cursor: pointer;
  border: none;
  font-family: inherit;
}

/* Sizes */
.base-button--small {
  padding: 8px 16px;
  font-size: 14px;
}

.base-button--medium {
  padding: 12px 24px;
  font-size: 16px;
}

.base-button--large {
  padding: 16px 32px;
  font-size: 18px;
}

/* Variants */
.base-button--primary {
  background: linear-gradient(135deg, var(--civic-primary, #2563eb) 0%, var(--civic-secondary, #1e40af) 100%);
  color: white;
  box-shadow: 0 4px 6px -1px rgba(37, 99, 235, 0.1), 0 2px 4px -1px rgba(37, 99, 235, 0.06);
}

.base-button--primary:hover:not(:disabled) {
  transform: translateY(-1px);
  box-shadow: 0 8px 12px -1px rgba(37, 99, 235, 0.2), 0 4px 6px -1px rgba(37, 99, 235, 0.1);
}

.base-button--secondary {
  background: white;
  color: var(--civic-text-primary, #1e293b);
  border: 1px solid #e2e8f0;
}

.base-button--secondary:hover:not(:disabled) {
  background: #f8fafc;
  border-color: #cbd5e1;
}

.base-button--text {
  background: transparent;
  color: var(--civic-text-secondary, #64748b);
  padding: 8px 12px;
}

.base-button--text:hover:not(:disabled) {
  background: #f1f5f9;
  color: var(--civic-text-primary, #1e293b);
}

/* States */
.base-button--block {
  width: 100%;
  display: flex;
}

.base-button--disabled {
  opacity: 0.6;
  cursor: not-allowed;
  pointer-events: none;
  filter: grayscale(0.5);
}
</style>
