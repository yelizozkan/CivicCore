<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Meslek Bilgileri</h2>
    </div>

    <v-form ref="form" v-model="isValid">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-1">

        <!-- Eğitim Durumu -->
        <div>
          <label class="field-label">Eğitim Durumu <span class="required">*</span></label>
          <v-select
            v-model="formData.educationLevel"
            :items="educationLevels"
            :rules="[rules.required]"
            variant="outlined"
            density="comfortable"
            placeholder="Seçiniz"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Meslek -->
        <div>
          <label class="field-label">Meslek <span class="required">*</span></label>
          <v-text-field
            v-model="formData.profession"
            :rules="[rules.required]"
            variant="outlined"
            density="comfortable"
            placeholder="Mesleğiniz"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Çalıştığı Kurum -->
        <div>
          <label class="field-label">Çalıştığı Kurum</label>
          <v-text-field
            v-model="formData.workplaceName"
            variant="outlined"
            density="comfortable"
            placeholder="Kurum / Şirket adı (opsiyonel)"
            hide-details="auto"
            class="mb-4"
          />
        </div>

        <!-- Görevi / Unvanı -->
        <div>
          <label class="field-label">Görevi / Unvanı</label>
          <v-text-field
            v-model="formData.workplacePosition"
            variant="outlined"
            density="comfortable"
            placeholder="Göreviniz (opsiyonel)"
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

const educationLevels = ['İlköğretim', 'Lise', 'Ön Lisans', 'Lisans', 'Yüksek Lisans', 'Doktora']

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