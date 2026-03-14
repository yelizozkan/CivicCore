<template>
  <div class="register-page">
    <div class="register-container">
      <div class="register-card">
        <!-- Logo & Header -->
        <div class="register-header">
          <div class="logo-container">
            <svg width="48" height="48" viewBox="0 0 24 24" fill="none">
              <circle cx="12" cy="12" r="10" fill="white" opacity="0.2"/>
              <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 3c1.66 0 3 1.34 3 3s-1.34 3-3 3-3-1.34-3-3 1.34-3 3-3zm0 14.2c-2.5 0-4.71-1.28-6-3.22.03-1.99 4-3.08 6-3.08 1.99 0 5.97 1.09 6 3.08-1.29 1.94-3.5 3.22-6 3.22z" fill="white"/>
            </svg>
          </div>
          <h1 class="register-title">BUSADER'e HOŞGELDİNİZ</h1>
          <p class="register-subtitle">Derneğimizin bir üyesi olmaya ne dersiniz ?</p>
        </div>

        <!-- Wizard -->
        <RegisterWizard />

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
import RegisterWizard from '~/components/Membership/RegisterWizard.vue'
//#endregion

//#region Page Metadata
definePageMeta({
  layout: false
})
//#endregion

//#region SEO
useHead({
  title: 'BUSADER - Üyelik Başvurusu',
  meta: [
    { name: 'description', content: 'BUSADER derneğine üyelik başvurusu yapın' }
  ]
})
//#endregion
</script>

<style scoped>
.register-page {
  min-height: 100vh;
  background: linear-gradient(145deg, #f7fdf9 0%, #f9fafb 40%, #f7fdf9 100%);
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
    radial-gradient(circle at 15% 15%, rgba(59, 130, 246, 0.04) 0%, transparent 45%),
    radial-gradient(circle at 85% 85%, rgba(37, 99, 235, 0.04) 0%, transparent 45%);
  pointer-events: none;
}

.register-container {
  width: 100%;
  max-width: 1100px;
  animation: fadeInUp 0.6s ease;
  position: relative;
  z-index: 1;
}

.register-card {
  background: linear-gradient(180deg, #ffffff 0%, #fdfffe 100%);
  border: 1px solid rgba(59, 130, 246, 0.08);
  box-shadow: 
    0 4px 6px rgba(59, 130, 246, 0.02),
    0 12px 24px rgba(0, 0, 0, 0.03),
    0 24px 48px rgba(59, 130, 246, 0.04);
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
  background: linear-gradient(135deg, #3b82f6 0%, #2563eb 100%);
  margin-bottom: 24px;
  box-shadow: 0 10px 25px rgba(59, 130, 246, 0.3);
  animation: pulse 3s infinite;
}

@keyframes pulse {
  0%, 100% { transform: scale(1); box-shadow: 0 10px 25px rgba(59, 130, 246, 0.3); }
  50% { transform: scale(1.03); box-shadow: 0 15px 35px rgba(59, 130, 246, 0.4); }
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
  font-weight: 500;
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
    border-radius: 20px;
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