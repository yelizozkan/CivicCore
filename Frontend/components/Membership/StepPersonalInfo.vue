<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Kişisel Bilgiler</h2>
    </div>

    <v-form ref="form" v-model="isValid">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-1">

        <!-- Ad -->
        <div>
          <label class="field-label">Ad <span class="required">*</span></label>
          <v-text-field
            v-model="formData.firstName"
            :rules="[rules.required, rules.onlyLetters, rules.minLength(2)]"
            variant="outlined"
            density="comfortable"
            placeholder="Adınız"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Soyad -->
        <div>
          <label class="field-label">Soyad <span class="required">*</span></label>
          <v-text-field
            v-model="formData.lastName"
            :rules="[rules.required, rules.onlyLetters, rules.minLength(2)]"
            variant="outlined"
            density="comfortable"
            placeholder="Soyadınız"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- TC Kimlik -->
        <div class="md:col-span-2">
          <label class="field-label">T.C. Kimlik Numarası <span class="required">*</span></label>
          <v-text-field
            v-model="formData.identityNumber"
            :rules="[rules.required, rules.tcKimlik]"
            variant="outlined"
            density="comfortable"
            placeholder="11 haneli kimlik numarası"
            maxlength="11"
            hide-details="auto"
            class="mb-4"
            @input="(e: any) => formData.identityNumber = e.target.value.replace(/\D/g, '')"
          />
        </div>

        <!-- Anne Adı -->
        <div>
          <label class="field-label">Anne Adı <span class="required">*</span></label>
          <v-text-field
            v-model="formData.motherName"
            :rules="[rules.required, rules.onlyLetters]"
            variant="outlined"
            density="comfortable"
            placeholder="Anne adı"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Baba Adı -->
        <div>
          <label class="field-label">Baba Adı <span class="required">*</span></label>
          <v-text-field
            v-model="formData.fatherName"
            :rules="[rules.required, rules.onlyLetters]"
            variant="outlined"
            density="comfortable"
            placeholder="Baba adı"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Doğum Yeri -->
        <div>
          <label class="field-label">Doğum Yeri <span class="required">*</span></label>
          <v-text-field
            v-model="formData.birthPlace"
            :rules="[rules.required]"
            variant="outlined"
            density="comfortable"
            placeholder="Doğum yeri"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Doğum Tarihi -->
        <div>
          <label class="field-label">Doğum Tarihi <span class="required">*</span></label>
          <v-text-field
            v-model="formData.birthDate"
            :rules="[rules.required]"
            type="date"
            variant="outlined"
            density="comfortable"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Kan Grubu -->
        <div class="md:col-span-2">
          <label class="field-label">Kan Grubu <span class="required">*</span></label>
          <v-select
            v-model="formData.bloodType"
            :items="bloodTypes"
            :rules="[rules.required]"
            variant="outlined"
            density="comfortable"
            placeholder="Seçiniz"
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

const bloodTypes = ['A Rh+', 'A Rh-', 'B Rh+', 'B Rh-', 'AB Rh+', 'AB Rh-', '0 Rh+', '0 Rh-']

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