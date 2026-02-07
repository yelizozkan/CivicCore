<template>
  <div class="wizard-container">
    <div v-if="isLoading" class="loading-state">
      <div class="spinner"></div>
      <p>Başvurunuz işleniyor...</p>
    </div>

    <div v-else-if="showSuccess">
      <MembershipSuccess />
    </div>

    <div v-else>
      <ProgressIndicator :current-step="currentStep" :total-steps="totalSteps" />

      <div class="step-wrapper">
        <Transition :name="transitionName" mode="out-in">
          <component 
            :is="currentStepComponent" 
            v-model="formData"
            ref="currentStepRef"
            @edit="handleEdit"
            :formData="formData" 
          />
        </Transition>
      </div>

      <div class="button-group">
        <button v-if="currentStep > 1" type="button" class="btn-back" @click="prevStep">
          ← Geri
        </button>
        <div v-else class="spacer"></div>

        <button type="button" class="btn-next" @click="handleNext" :disabled="isLoading">
          <span v-if="isLoading" class="btn-spinner"></span>
          <template v-else>
            {{ currentStep === totalSteps ? 'Başvuruyu Gönder ✓' : 'Sonraki Adım →' }}
          </template>
        </button>
      </div>
    </div>

    <!-- Error Toast -->
    <Transition name="slide-up">
      <div v-if="error" class="error-toast">
        <span>⚠️ {{ error }}</span>
        <button @click="error = ''">&times;</button>
      </div>
    </Transition>

    <!-- Success Toast -->
    <Transition name="slide-up">
      <div v-if="successMessage" class="success-toast">
        <span>✅ {{ successMessage }}</span>
        <button @click="successMessage = ''">&times;</button>
      </div>
    </Transition>
  </div>
</template>

<script setup lang="ts">
//#region Imports
import { ref, reactive, computed } from 'vue'
import StepPersonalInfo from './StepPersonalInfo.vue'
import StepContactInfo from './StepContactInfo.vue'
import StepProfessional from './StepProfessional.vue'
import StepMotivation from './StepMotivation.vue'
import StepConsents from './StepConsents.vue'
import StepReview from './StepReview.vue'
import MembershipSuccess from './MembershipSuccess.vue'
import ProgressIndicator from './ProgressIndicator.vue'
//#endregion

//#region Types
interface MembershipFormData {
  firstName: string
  lastName: string
  identityNumber: string
  motherName: string
  fatherName: string
  birthPlace: string
  birthDate: string
  bloodType: string
  email: string
  mobilePhone: string
  workPhone: string
  residenceAddress: string
  workplaceAddress: string
  educationLevel: string
  profession: string
  workplaceName: string
  workplacePosition: string
  motivationText: string
  kvkkAccepted: boolean
  explicitConsentAccepted: boolean
}

interface GroupInfo {
  type: string
  tenantId: number
  tenantGroupId: number | null
  name: string
  tenantName: string
  slug: string
}

type StepDirection = 'right' | 'left'
//#endregion

//#region Props
const props = defineProps<{
  tenantGroupId: number
  groupInfo?: GroupInfo
}>()
//#endregion

//#region Composables
const { createMembership } = useMemberships()
//#endregion

//#region Reactive State
const currentStep = ref(1)
const totalSteps = 6
const direction = ref<StepDirection>('right')
const isLoading = ref(false)
const showSuccess = ref(false)
const currentStepRef = ref<any>(null)
const error = ref('')
const successMessage = ref('')

const formData = reactive<MembershipFormData>({
  firstName: '',
  lastName: '',
  identityNumber: '',
  motherName: '',
  fatherName: '',
  birthPlace: '',
  birthDate: '',
  bloodType: '',
  email: '',
  mobilePhone: '',
  workPhone: '',
  residenceAddress: '',
  workplaceAddress: '',
  educationLevel: '',
  profession: '',
  workplaceName: '',
  workplacePosition: '',
  motivationText: '',
  kvkkAccepted: false,
  explicitConsentAccepted: false
})
//#endregion

//#region Computed
const currentStepComponent = computed(() => {
  switch (currentStep.value) {
    case 1: return StepPersonalInfo
    case 2: return StepContactInfo
    case 3: return StepProfessional
    case 4: return StepMotivation
    case 5: return StepConsents
    case 6: return StepReview
    default: return StepPersonalInfo
  }
})

const transitionName = computed(() => {
  return direction.value === 'right' ? 'slide-left' : 'slide-right'
})
//#endregion

//#region Methods
const scrollToTop = () => {
  if (process.client) {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }
}

const showError = (message: string) => {
  error.value = message
  setTimeout(() => error.value = '', 5000)
}

const showSuccessToast = (message: string) => {
  successMessage.value = message
  setTimeout(() => successMessage.value = '', 3000)
}

const handleNext = async () => {
  // Validate current step
  if (currentStepRef.value && typeof currentStepRef.value.validate === 'function') {
    const isValid = await currentStepRef.value.validate()
    if (!isValid) {
      showError('Lütfen tüm zorunlu alanları doldurun.')
      return
    }
  }

  if (currentStep.value < totalSteps) {
    direction.value = 'right'
    currentStep.value++
    scrollToTop()
  } else {
    await submitForm()
  }
}

const prevStep = () => {
  if (currentStep.value > 1) {
    direction.value = 'left'
    currentStep.value--
    scrollToTop()
  }
}

const handleEdit = (step: number) => {
  direction.value = 'left'
  currentStep.value = step
  scrollToTop()
}

const submitForm = async () => {
  isLoading.value = true
  error.value = ''
  
  try {
    // Prepare payload matching backend DTO
    const payload = {
      firstName: formData.firstName,
      lastName: formData.lastName,
      identityNumber: formData.identityNumber,
      motherName: formData.motherName,
      fatherName: formData.fatherName,
      birthPlace: formData.birthPlace,
      birthDate: formData.birthDate,
      bloodType: formData.bloodType,
      educationLevel: formData.educationLevel,
      profession: formData.profession,
      workplaceName: formData.workplaceName || null,
      workplacePosition: formData.workplacePosition || null,
      mobilePhone: formData.mobilePhone,
      workPhone: formData.workPhone || null,
      email: formData.email,
      residenceAddress: formData.residenceAddress,
      workplaceAddress: formData.workplaceAddress || null,
      motivationText: formData.motivationText,
      kvkkAccepted: formData.kvkkAccepted,
      explicitConsentAccepted: formData.explicitConsentAccepted
    }

    // 🔥 Use tenantGroupId from props (resolved from slug)
    await createMembership(payload, props.tenantGroupId)

    // Success
    showSuccess.value = true
    scrollToTop()
    
  } catch (err: any) {
    console.error('Başvuru gönderilemedi:', err)
    
    // Handle different error formats
    let errorMessage = 'Başvuru gönderilemedi. Lütfen tekrar deneyin.'
    
    if (err.data?.error) {
      errorMessage = err.data.error
    } else if (err.data?.message) {
      errorMessage = err.data.message
    } else if (err.data?.errors && Array.isArray(err.data.errors)) {
      errorMessage = err.data.errors.join(', ')
    } else if (err.message) {
      errorMessage = err.message
    }
    
    showError(errorMessage)
  } finally {
    isLoading.value = false
  }
}
//#endregion
</script>

<style scoped>
.wizard-container {
  width: 100%;
  position: relative;
}

.loading-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 60px 20px;
  text-align: center;
}

.spinner {
  width: 48px;
  height: 48px;
  border: 3px solid rgba(255, 255, 255, 0.1);
  border-top-color: #3b82f6;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.loading-state p {
  margin-top: 16px;
  font-size: 14px;
  color: rgba(255, 255, 255, 0.5);
}

.step-wrapper {
  min-height: 350px;
  position: relative;
}

.button-group {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 28px;
  padding-top: 20px;
  border-top: 1px solid rgba(255, 255, 255, 0.08);
  gap: 16px;
}

.spacer {
  flex: 1;
}

.btn-back {
  padding: 12px 24px;
  font-size: 14px;
  font-weight: 600;
  border-radius: 10px;
  cursor: pointer;
  transition: all 0.2s ease;
  border: 1px solid rgba(255, 255, 255, 0.15);
  background-color: rgba(255, 255, 255, 0.05);
  color: rgba(255, 255, 255, 0.7);
}

.btn-back:hover {
  background-color: rgba(255, 255, 255, 0.1);
  border-color: rgba(255, 255, 255, 0.3);
}

.btn-next {
  padding: 12px 28px;
  font-size: 14px;
  font-weight: 600;
  border-radius: 10px;
  cursor: pointer;
  transition: all 0.2s ease;
  border: none;
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  color: white;
  box-shadow: 0 4px 15px rgba(59, 130, 246, 0.35);
}

.btn-next:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(59, 130, 246, 0.45);
}

.btn-next:disabled {
  opacity: 0.6;
  cursor: not-allowed;
  transform: none;
}

.btn-spinner {
  width: 16px;
  height: 16px;
  border: 2px solid transparent;
  border-top-color: white;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}

/* Toast Notifications */
.error-toast,
.success-toast {
  position: fixed;
  bottom: 24px;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 12px 18px;
  border-radius: 10px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
  z-index: 1000;
}

.error-toast {
  background-color: rgba(239, 68, 68, 0.15);
  border: 1px solid rgba(239, 68, 68, 0.3);
}

.error-toast span {
  font-size: 13px;
  font-weight: 500;
  color: #f87171;
}

.error-toast button {
  padding: 2px 6px;
  background: none;
  border: none;
  font-size: 18px;
  color: #f87171;
  cursor: pointer;
}

.success-toast {
  background-color: rgba(16, 185, 129, 0.15);
  border: 1px solid rgba(16, 185, 129, 0.3);
}

.success-toast span {
  font-size: 13px;
  font-weight: 500;
  color: #10b981;
}

.success-toast button {
  padding: 2px 6px;
  background: none;
  border: none;
  font-size: 18px;
  color: #10b981;
  cursor: pointer;
}

/* Transitions */
.slide-left-enter-active,
.slide-left-leave-active,
.slide-right-enter-active,
.slide-right-leave-active {
  transition: all 0.25s ease;
}

.slide-left-enter-from { opacity: 0; transform: translateX(20px); }
.slide-left-leave-to { opacity: 0; transform: translateX(-20px); }
.slide-right-enter-from { opacity: 0; transform: translateX(-20px); }
.slide-right-leave-to { opacity: 0; transform: translateX(20px); }

.slide-up-enter-active,
.slide-up-leave-active {
  transition: all 0.3s ease;
}

.slide-up-enter-from,
.slide-up-leave-to {
  opacity: 0;
  transform: translate(-50%, 20px);
}
</style>