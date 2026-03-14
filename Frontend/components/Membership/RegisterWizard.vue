<template>
  <div class="wizard-container">
    <div v-if="isSubmitting && currentStep !== totalSteps" class="loading-state">
      <div class="spinner"></div>
      <p>Başvurunuz işleniyor...</p>
    </div>

    <div v-else-if="showSuccess">
      <MembershipSuccess />
    </div>

    <div v-else>
      <ProgressIndicator 
        :current-step="currentStep" 
        :total-steps="totalSteps" 
        @step-click="handleEdit" 
      />

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

      <div class="flex justify-between items-center mt-10 pt-6 border-t border-slate-200 gap-4">
        <!-- Geri Butonu -->
        <button 
          v-if="currentStep > 1"
          @click="prevStep"
          class="flex items-center gap-2 px-6 py-3 text-slate-600 hover:text-slate-800 hover:bg-slate-100 rounded-xl transition-all text-base font-medium"
        >
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 19l-7-7 7-7"/>
          </svg>
          Geri
        </button>
        <div v-else class="flex-1"></div>

        <!-- Son adımda Gönder Butonu -->
        <button 
          v-if="currentStep === totalSteps"
          @click="submitForm"
          :disabled="isSubmitting"
          class="flex items-center gap-2 px-8 py-3 bg-emerald-600 hover:bg-emerald-700 disabled:bg-slate-300 text-white rounded-xl transition-all text-base font-medium shadow-sm ml-auto"
        >
          <span v-if="isSubmitting">Gönderiliyor...</span>
          <span v-else>Başvuruyu Gönder</span>
          <svg v-if="!isSubmitting" class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"/>
          </svg>
        </button>

        <!-- Devam Et Butonu -->
        <button 
          v-else
          @click="handleNext"
          class="flex items-center gap-2 px-8 py-3 bg-blue-600 hover:bg-blue-700 disabled:bg-slate-300 disabled:cursor-not-allowed text-white rounded-xl transition-all text-base font-medium shadow-sm ml-auto"
        >
          Devam Et
          <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"/>
          </svg>
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
import { useMemberships } from '~/composables/useMemberships'
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
import { useRouter } from 'vue-router'
const { createMembership } = useMemberships()
const router = useRouter()
//#endregion

//#region Reactive State
const currentStep = ref(1)
const totalSteps = 6
const direction = ref<StepDirection>('right')
const isSubmitting = ref(false)
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
  if (step < currentStep.value) {
     direction.value = 'left'
     currentStep.value = step
     scrollToTop()
  } else if (step > currentStep.value) {
      // In case we want to support jumping forward if already visited
      // For now disable forward jump unless it is implemented
      direction.value = 'right'
      currentStep.value = step
      scrollToTop()
  }
}

const submitForm = async () => {
  if (currentStepRef.value && typeof currentStepRef.value.validate === 'function') {
    const isValid = await currentStepRef.value.validate()
    if (!isValid) {
      showError('Lütfen tüm zorunlu alanları doldurun.')
      return
    }
  }

  isSubmitting.value = true
  error.value = ''
  
  try {
    const payload = {
      tenantGroupId: props.tenantGroupId,
      membership: {
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
    }

    await createMembership(payload, props.tenantGroupId)
    
    // Yönlendirme
    router.push('/membership/success')
    
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
    isSubmitting.value = false
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
  border: 3px solid rgba(59, 130, 246, 0.2);
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
  color: #64748b;
}

.step-wrapper {
  min-height: 350px;
  position: relative;
  padding: 10px 0;
  max-width: 700px;
  margin: 0 auto;
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
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
  z-index: 1000;
}

.error-toast {
  background-color: #fef2f2;
  border: 1px solid #fee2e2;
}

.error-toast span {
  font-size: 13px;
  font-weight: 500;
  color: #ef4444;
}

.error-toast button {
  padding: 2px 6px;
  background: none;
  border: none;
  font-size: 18px;
  color: #ef4444;
  cursor: pointer;
}

.success-toast {
  background-color: #ecfdf5;
  border: 1px solid #d1fae5;
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