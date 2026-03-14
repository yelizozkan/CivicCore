<template>
  <div class="register-page">
    <!-- Loading State -->
    <div v-if="isLoading" class="loading-container">
      <div class="spinner"></div>
      <p>Yükleniyor...</p>
    </div>

    <!-- Error State - Invalid Slug -->
    <div v-else-if="error" class="error-container">
      <div class="error-card">
        <div class="error-icon">❌</div>
        <h2>Geçersiz Kayıt Linki</h2>
        <p>{{ error }}</p>
        <a href="/" class="home-link">Ana Sayfaya Dön</a>
      </div>
    </div>

    <!-- Success - Show Registration Form -->
    <div v-else-if="groupInfo" class="register-container">
      <div class="register-card">
        <!-- Logo & Header -->
        <div class="register-header">
          <div class="logo-container">
            <svg width="48" height="48" viewBox="0 0 24 24" fill="none">
              <circle cx="12" cy="12" r="10" fill="white" opacity="0.2"/>
              <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 3c1.66 0 3 1.34 3 3s-1.34 3-3 3-3-1.34-3-3 1.34-3 3-3zm0 14.2c-2.5 0-4.71-1.28-6-3.22.03-1.99 4-3.08 6-3.08 1.99 0 5.97 1.09 6 3.08-1.29 1.94-3.5 3.22-6 3.22z" fill="white"/>
            </svg>
          </div>
          <h1 class="register-title">{{ groupInfo.name }}</h1>
          <p class="register-subtitle">Üyelik Başvuru Formu</p>
          <div class="tenant-badge" v-if="groupInfo.tenantName && groupInfo.type === 'group'">
            {{ groupInfo.tenantName }}
          </div>
        </div>

        <!-- Wizard Component -->
        <RegisterWizard 
          :tenant-group-id="groupInfo.tenantGroupId || groupInfo.tenantId"
          :group-info="groupInfo"
        />
      </div>

      <!-- Footer -->
      <!-- <div class="register-footer">
        <p>Zaten üye misiniz? <a href="/" class="link">Giriş Yapın</a></p>
      </div> -->
    </div>
  </div>
</template>

<script setup lang="ts">
//#region Imports
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import RegisterWizard from '~/components/Membership/RegisterWizard.vue'
//#endregion

//#region Page Metadata
definePageMeta({
  layout: false
})
//#endregion

//#region Composables
const route = useRoute()
const { resolveSlug } = useMemberships()
//#endregion

//#region Reactive State
const isLoading = ref(true)
const error = ref<string | null>(null)
const groupInfo = ref<{
  type: string
  tenantId: number
  tenantGroupId: number | null
  name: string
  tenantName: string
  slug: string
} | null>(null)
//#endregion

//#region SEO
useHead({
  title: () => groupInfo.value ? `${groupInfo.value.name} - Üyelik Başvurusu` : 'Üyelik Başvurusu',
  meta: [
    { name: 'description', content: 'Derneğe üyelik başvurusu yapın' }
  ]
})
//#endregion

//#region Lifecycle
onMounted(async () => {
  const slug = route.params.slug as string
  
  if (!slug) {
    error.value = 'Kayıt linki bulunamadı.'
    isLoading.value = false
    return
  }

  try {
    // API'den slug'ı resolve et
    const response = await resolveSlug(slug)
    groupInfo.value = response
    console.log('Group info loaded:', response)
  } catch (err: any) {
    console.error('Slug resolve error:', err)
    
    if (err.status === 404 || err.statusCode === 404) {
      error.value = 'Bu kayıt linki geçersiz veya süresi dolmuş.'
    } else {
      error.value = 'Bir hata oluştu. Lütfen daha sonra tekrar deneyin.'
    }
  } finally {
    isLoading.value = false
  }
})
//#endregion
</script>

<style scoped>
.register-page {
  min-height: 100vh;
  background: linear-gradient(135deg, #f0fdf4 0%, #f8fafc 50%, #f0fdf4 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 40px 20px;
  position: relative;
  overflow-x: hidden;
}

.register-page::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-image: 
    radial-gradient(circle at 20% 20%, rgba(59, 130, 246, 0.06) 0%, transparent 50%),
    radial-gradient(circle at 80% 80%, rgba(37, 99, 235, 0.06) 0%, transparent 50%),
    radial-gradient(circle at 50% 50%, rgba(96, 165, 250, 0.04) 0%, transparent 40%);
  pointer-events: none;
}

/* Loading State */
.loading-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  color: #475569;
  z-index: 1;
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

.loading-container p {
  margin-top: 16px;
  font-size: 14px;
  color: #64748b;
}

/* Error State */
.error-container {
  z-index: 1;
}

.error-card {
  background: white;
  border: 1px solid #fee2e2;
  border-radius: 24px;
  padding: 48px;
  text-align: center;
  max-width: 400px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.05);
}

.error-icon {
  font-size: 64px;
  margin-bottom: 20px;
}

.error-card h2 {
  font-size: 24px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 12px;
}

.error-card p {
  font-size: 14px;
  color: #64748b;
  margin-bottom: 24px;
}

.home-link {
  display: inline-block;
  padding: 12px 24px;
  background: linear-gradient(135deg, #3b82f6 0%, #06b6d4 100%);
  color: white;
  text-decoration: none;
  border-radius: 10px;
  font-weight: 600;
  transition: all 0.2s;
}

.home-link:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 15px rgba(59, 130, 246, 0.4);
}

/* Register Container */
.register-container {
  width: 100%;
  max-width: 800px;
  animation: fadeInUp 0.6s ease;
  position: relative;
  z-index: 1;
}

.register-card {
  background: white;
  border: 1px solid #e2e8f0;
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.05);
  padding: 48px;
  border-radius: 24px;
}

/* Header */
.register-header {
  text-align: center;
  margin-bottom: 40px;
}

.logo-container {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 80px;
  height: 80px;
  border-radius: 20px;
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  margin-bottom: 24px;
  box-shadow: 0 10px 25px rgba(59, 130, 246, 0.25);
}

.register-title {
  font-size: 28px;
  font-weight: 800;
  color: #1e293b;
  margin-bottom: 8px;
  letter-spacing: -0.5px;
}

.register-subtitle {
  font-size: 16px;
  color: #64748b;
  margin-bottom: 12px;
  font-weight: 500;
}

.tenant-badge {
  display: inline-block;
  padding: 6px 16px;
  background: #eff6ff;
  border: 1px solid #dbeafe;
  border-radius: 20px;
  font-size: 12px;
  font-weight: 600;
  color: #3b82f6;
}

/* Footer - Hidden */
.register-footer {
  display: none;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@media (max-width: 768px) {
  .register-card {
    padding: 32px 20px;
  }
  
  .register-title {
    font-size: 24px;
  }
  
  .logo-container {
    width: 64px;
    height: 64px;
    margin-bottom: 20px;
  }
}
</style>