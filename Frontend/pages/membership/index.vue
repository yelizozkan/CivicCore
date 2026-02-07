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
      <div class="register-footer">
        <p>Zaten üye misiniz? <a href="/" class="link">Giriş Yapın</a></p>
      </div>
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
  background: linear-gradient(135deg, #1e293b 0%, #0f172a 50%, #1e293b 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 40px 20px;
  position: relative;
}

/* Subtle pattern overlay */
.register-page::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-image: 
    radial-gradient(circle at 25% 25%, rgba(59, 130, 246, 0.1) 0%, transparent 50%),
    radial-gradient(circle at 75% 75%, rgba(20, 184, 166, 0.1) 0%, transparent 50%);
  pointer-events: none;
}

.register-container {
  width: 100%;
  max-width: 900px;
  animation: fadeInUp 0.6s ease;
  position: relative;
  z-index: 1;
}

.register-card {
  background: rgba(30, 41, 59, 0.8);
  backdrop-filter: blur(20px);
  border: 1px solid rgba(255, 255, 255, 0.1);
  box-shadow: 0 25px 50px rgba(0, 0, 0, 0.4);
  padding: 48px;
  border-radius: 24px;
}

/* Header */
.register-header {
  text-align: center;
  margin-bottom: 32px;
}

.logo-container {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 80px;
  height: 80px;
  border-radius: 20px;
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  margin-bottom: 20px;
  box-shadow: 0 8px 32px rgba(59, 130, 246, 0.4);
  animation: pulse 2s infinite;
}

@keyframes pulse {
  0%, 100% { transform: scale(1); box-shadow: 0 8px 32px rgba(59, 130, 246, 0.4); }
  50% { transform: scale(1.05); box-shadow: 0 12px 40px rgba(59, 130, 246, 0.6); }
}

.register-title {
  font-size: 28px;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 8px;
  letter-spacing: -0.5px;
}

.register-subtitle {
  font-size: 15px;
  color: rgba(255, 255, 255, 0.6);
}

/* Footer */
.register-footer {
  text-align: center;
  margin-top: 24px;
}

.register-footer p {
  font-size: 14px;
  color: rgba(255, 255, 255, 0.5);
}

.link {
  color: #3b82f6;
  font-weight: 600;
  text-decoration: none;
  transition: all 0.2s;
}

.link:hover {
  color: #14b8a6;
  text-decoration: underline;
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
    width: 70px;
    height: 70px;
  }
}
</style>