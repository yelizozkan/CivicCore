<template>
  <div class="progress-container">
    <div 
      v-for="(step, index) in steps" 
      :key="index"
      class="step-item"
      :class="{ 
        'active': currentStep === index + 1,
        'completed': currentStep > index + 1,
        'clickable': true
      }"
      @click="handleStepClick(index)"
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

const props = defineProps<Props>()
const emit = defineEmits(['step-click'])

const steps = ['Kişisel', 'İletişim', 'Meslek', 'Motivasyon', 'Onaylar', 'Özet']

const handleStepClick = (index: number) => {
  const targetStep = index + 1
  // Herhangi bir step'e tıklanabilir (ileri veya geri)
  if (targetStep !== props.currentStep) {
    emit('step-click', targetStep)
  }
}
</script>

<style scoped>
.progress-container {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 32px;
  padding: 0 10px;
}

.step-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  flex: 1;
  cursor: default;
}

.step-item.completed {
  cursor: pointer;
}

.step-circle {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 600;
  font-size: 15px;
  background-color: #f8fafc;
  color: #94a3b8;
  transition: all 0.3s ease;
  position: relative;
  z-index: 2;
  border: 2px solid #e2e8f0;
}

/* Aktif adım - Mavi */
.step-item.active .step-circle {
  background: white;
  color: #3b82f6;
  border-color: #3b82f6;
  box-shadow: 0 0 0 4px rgba(59, 130, 246, 0.15);
  transform: scale(1.1);
}

/* Tamamlanmış adım - Mavi */
.step-item.completed .step-circle {
  background: linear-gradient(135deg, #3b82f6 0%, #2563eb 100%);
  color: white;
  border-color: #3b82f6;
}

.step-item.completed:hover .step-circle {
  background: linear-gradient(135deg, #2563eb 0%, #1d4ed8 100%);
  border-color: #2563eb;
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(59, 130, 246, 0.25);
}

.check-icon {
  width: 18px;
  height: 18px;
}

.step-label {
  margin-top: 10px;
  font-size: 13px;
  font-weight: 600;
  color: #94a3b8;
  text-align: center;
  transition: all 0.3s ease;
}

.step-item.active .step-label {
  color: #1e293b;
  font-weight: 700;
}

.step-item.completed .step-label {
  color: #3b82f6;
}

/* Çizgi - Mavi gradient */
.step-line {
  position: absolute;
  top: 22px;
  left: calc(50% + 22px);
  width: calc(100% - 44px);
  height: 3px;
  background-color: #e2e8f0;
  z-index: 1;
  transition: all 0.5s ease;
  border-radius: 2px;
}

.step-line.completed {
  background: linear-gradient(90deg, #3b82f6 0%, #2563eb 100%);
}

@media (max-width: 768px) {
  .progress-container {
    padding: 0;
    margin-bottom: 24px;
  }
  
  .step-circle {
    width: 36px;
    height: 36px;
    font-size: 13px;
  }
  
  .step-label {
    font-size: 11px;
    display: none;
  }
  
  .step-item.active .step-label {
    display: block;
    position: absolute;
    bottom: -20px;
    width: 100px;
  }

  .step-line {
    top: 18px;
    left: calc(50% + 18px);
    width: calc(100% - 36px);
  }
}
</style>