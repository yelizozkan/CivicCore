<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">İletişim Bilgileri</h2>
    </div>

    <v-form ref="form" v-model="isValid">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-1">

        <!-- Cep Telefonu -->
        <div>
          <label class="field-label">Cep Telefonu <span class="required">*</span></label>
          <v-text-field
            v-model="formData.mobilePhone"
            :rules="[rules.required, rules.turkishPhone]"
            variant="outlined"
            density="comfortable"
            placeholder="+90 5XX XXX XX XX"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- İş Telefonu -->
        <div>
          <label class="field-label">İş Telefonu</label>
          <v-text-field
            v-model="formData.workPhone"
            variant="outlined"
            density="comfortable"
            placeholder="İş telefonu (opsiyonel)"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- E-posta -->
        <div class="md:col-span-2">
          <label class="field-label">E-posta Adresi <span class="required">*</span></label>
          <v-text-field
            v-model="formData.email"
            :rules="[rules.required, rules.email]"
            type="email"
            variant="outlined"
            density="comfortable"
            placeholder="ornek@email.com"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- İkamet Adresi -->
        <div class="md:col-span-2">
          <label class="field-label">İkamet Adresi <span class="required">*</span></label>
          <v-textarea
            v-model="formData.residenceAddress"
            :rules="[rules.required]"
            variant="outlined"
            density="comfortable"
            placeholder="Ev adresiniz"
            rows="2"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- İş Adresi -->
        <div class="md:col-span-2">
          <label class="field-label">İş Adresi</label>
          <v-textarea
            v-model="formData.workplaceAddress"
            variant="outlined"
            density="comfortable"
            placeholder="İş adresiniz (opsiyonel)"
            rows="2"
            hide-details="auto"
            class="mb-4"
          />
        </div>

      </div>
    </v-form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const props = defineProps<{
  formData: any
}>()

const { validationRules: rules } = useValidators()
const form = ref()
const isValid = ref(false)

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
</style>