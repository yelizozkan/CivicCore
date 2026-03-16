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
            :form-data="formData"
            ref="currentStepRef"
            @edit="handleEdit"
          />
        </Transition>
      </div>

      <div class="flex justify-between items-center mt-10 pt-6 border-t border-slate-200 gap-4">
        <!-- Geri Butonu -->
        <button 
          v-if="currentStep > 1"
          @click="prevStep"
          class="flex items-center gap-2 px-6 py-2.5 text-slate-600 hover:text-slate-800 hover:bg-slate-100 rounded-lg transition-all text-sm font-medium"
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
          class="flex items-center gap-2 px-6 py-2.5 bg-emerald-600 hover:bg-emerald-700 disabled:bg-slate-300 text-white rounded-lg transition-all text-sm font-medium shadow-sm ml-auto"
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
          class="flex items-center gap-2 px-6 py-2.5 bg-blue-600 hover:bg-blue-700 disabled:bg-slate-300 disabled:cursor-not-allowed text-white rounded-lg transition-all text-sm font-medium shadow-sm ml-auto"
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

const formData = ref<MembershipFormData>({
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
  // Validate current step using async validate()
  if (currentStepRef.value && typeof currentStepRef.value.validate === 'function') {
    const isValid = await currentStepRef.value.validate()
    if (!isValid) {
      // Vuetify shows inline errors, so no toast needed for steps 1-5
      // Still show for consents which use custom error display
      if (currentStep.value === 5) {
        showError('Lütfen tüm onayları verin.')
      }
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
    // DEBUG: Check Tenant Group ID
    console.log('=== TENANT GROUP DEBUG ===')
    console.log('props.tenantGroupId:', props.tenantGroupId)
    console.log('props.tenantGroupId type:', typeof props.tenantGroupId)
    console.log('props.groupInfo:', props.groupInfo)
    console.log('route.params:', router.currentRoute.value.params)
    console.log('===========================')

    // DEBUG: Check formData content
    console.log('=== FORM DATA DEBUG ===')
    console.log('formData:', formData)
    console.log('formData.value:', formData.value)
    console.log('firstName:', formData.value?.firstName)
    console.log('email:', formData.value?.email)
    console.log('========================')

    // Convert tenantGroupId to integer
    const tenantId = parseInt(props.tenantGroupId?.toString() || '0', 10)
    console.log('tenantGroupId (parsed):', tenantId, typeof tenantId)

    const membershipData = {
      firstName: formData.value.firstName,
      lastName: formData.value.lastName,
      identityNumber: formData.value.identityNumber,
      motherName: formData.value.motherName,
      fatherName: formData.value.fatherName,
      birthPlace: formData.value.birthPlace,
      birthDate: formData.value.birthDate,
      bloodType: formData.value.bloodType,
      educationLevel: formData.value.educationLevel,
      profession: formData.value.profession,
      workplaceName: formData.value.workplaceName || '',
      workplacePosition: formData.value.workplacePosition || '',
      mobilePhone: formData.value.mobilePhone,
      workPhone: formData.value.workPhone || '',
      email: formData.value.email,
      residenceAddress: formData.value.residenceAddress,
      workplaceAddress: formData.value.workplaceAddress || '',
      motivationText: formData.value.motivationText,
      kvkkAccepted: formData.value.kvkkAccepted,
      explicitConsentAccepted: formData.value.explicitConsentAccepted
    }

    console.log('=== PAYLOAD DEBUG ===')
    console.log('Membership Data to be wrapped:', JSON.stringify(membershipData, null, 2))
    console.log('=====================')

    const response = await createMembership(membershipData as any, tenantId)
    console.log('=== API SUCCESS ===')
    console.log('Response:', response)
    console.log('===================')
    
    // Yönlendirme
    router.push('/membership/success')
    
  } catch (err: any) {
    console.log('=== API ERROR ===')
    console.log('Error object:', err)
    console.log('Error.data:', err?.data)
    console.log('Error.message:', err?.message)
    console.log('Error.status:', err?.status || err?.statusCode)
    console.log('=================')
    
    // Hata mesajını göster
    if (err?.data?.errors) {
      const errorMessages = Object.values(err.data.errors).flat().join(', ')
      showError(errorMessages)
    } else if (err?.data?.title) {
      showError(err.data.title)
    } else {
      showError('Bağlantı hatası. Lütfen tüm alanları kontrol edin ve tekrar deneyin.')
    }
  } finally {
    isSubmitting.value = false
  }
}
//#endregion
</script>

<style scoped>
.wizard-container {
  width: 100%;
  max-width: 800px;
  margin: 0 auto;
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
  width: 100%;
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