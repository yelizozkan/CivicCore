<template>
  <div class="progress-container">
    <div 
      v-for="(step, index) in steps" 
      :key="index"
      class="step-item"
      :class="{ 
        'active': currentStep === index + 1,
        'completed': currentStep > index + 1
      }"
    >
      <div class="step-circle">
        <svg v-if="currentStep > index + 1" class="check-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3">
          <polyline points="20 6 9 17 4 12"></polyline>
        </svg>
        <span v-else>{{ index + 1 }}</span>
      </div>
      <span class="step-label">{{ step }}</span>
      <div v-if="index < steps.length - 1" class="step-line" :class="{ 'completed': currentStep > index + 1 }"></div>
    </div>
  </div>
</template>

<script setup lang="ts">
interface Props {
  currentStep: number
  totalSteps: number
}

defineProps<Props>()

const steps = ['Kişisel', 'İletişim', 'Meslek', 'Motivasyon', 'Onaylar', 'Özet']
</script>

<style scoped>
.progress-container {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 32px;
  padding: 20px 10px;
  background: rgba(15, 23, 42, 0.6);
  border-radius: 16px;
  border: 1px solid rgba(255, 255, 255, 0.05);
}

.step-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  flex: 1;
}

.step-circle {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 600;
  font-size: 16px;
  background-color: rgba(255, 255, 255, 0.1);
  color: rgba(255, 255, 255, 0.5);
  transition: all 0.3s ease;
  position: relative;
  z-index: 2;
  border: 2px solid rgba(255, 255, 255, 0.1);
}

/* Aktif adım */
.step-item.active .step-circle {
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  color: white;
  box-shadow: 0 0 20px rgba(59, 130, 246, 0.5);
  border-color: transparent;
}

/* Tamamlanmış adım */
.step-item.completed .step-circle {
  background-color: #10b981;
  color: white;
  border-color: #10b981;
}

.check-icon {
  width: 20px;
  height: 20px;
}

.step-label {
  margin-top: 8px;
  font-size: 11px;
  font-weight: 500;
  color: rgba(255, 255, 255, 0.4);
  text-align: center;
  transition: all 0.3s ease;
}

.step-item.active .step-label {
  color: #3b82f6;
  font-weight: 600;
}

.step-item.completed .step-label {
  color: #10b981;
}

.step-line {
  position: absolute;
  top: 22px;
  left: calc(50% + 28px);
  width: calc(100% - 56px);
  height: 2px;
  background-color: rgba(255, 255, 255, 0.1);
  z-index: 1;
  transition: all 0.3s ease;
}

.step-line.completed {
  background-color: #10b981;
}

@media (max-width: 768px) {
  .progress-container {
    padding: 16px 8px;
  }
  
  .step-circle {
    width: 36px;
    height: 36px;
    font-size: 14px;
  }
  
  .step-label {
    font-size: 9px;
  }
  
  .step-line {
    top: 18px;
    left: calc(50% + 22px);
    width: calc(100% - 44px);
  }
}
</style>