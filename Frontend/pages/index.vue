<template>
  <div class="login-root">

    <!-- ── LEFT PANEL ── -->
    <div class="left-panel">
      <!-- Ambient blobs -->
      <div class="blob blob-1"></div>
      <div class="blob blob-2"></div>
      <div class="blob blob-3"></div>

      <!-- Grid overlay -->
      <div class="grid-overlay"></div>

      <!-- Content -->
      <div class="left-content">
        <!-- Logo mark -->
        <div class="logo-mark">
          <svg width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/>
            <circle cx="9" cy="7" r="4"/>
            <path d="M23 21v-2a4 4 0 0 0-3-3.87"/>
            <path d="M16 3.13a4 4 0 0 1 0 7.75"/>
          </svg>
        </div>

        <div class="left-text">
          <div class="left-badge">Dernek Yönetim Sistemi</div>
          <h1 class="left-heading">
            Derneğinizi<br/>
            Kolayca Yönetin
          </h1>
          <p class="left-desc">
            Üyelik başvuruları, aidat takibi ve dernek işlemlerinizi
            tek bir platformdan profesyonelce yönetin.
          </p>
        </div>

        <!-- Stats row -->
        <div class="stats-row">
          <div class="stat">
            <span class="stat-value">500+</span>
            <span class="stat-label">Aktif Üye</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat">
            <span class="stat-value">%99</span>
            <span class="stat-label">Uptime</span>
          </div>
          <div class="stat-divider"></div>
          <div class="stat">
            <span class="stat-value">7/24</span>
            <span class="stat-label">Destek</span>
          </div>
        </div>

        <!-- Feature cards -->
        <div class="feature-list">
          <div class="feature-item">
            <div class="feature-icon">
              <svg width="16" height="16" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                <polyline points="22,4 12,14.01 9,11.01"/>
              </svg>
            </div>
            <span>Üyelik başvurularını anında takip edin</span>
          </div>
          <div class="feature-item">
            <div class="feature-icon">
              <svg width="16" height="16" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                <polyline points="22,4 12,14.01 9,11.01"/>
              </svg>
            </div>
            <span>Rol tabanlı yetki yönetimi</span>
          </div>
          <div class="feature-item">
            <div class="feature-icon">
              <svg width="16" height="16" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                <polyline points="22,4 12,14.01 9,11.01"/>
              </svg>
            </div>
            <span>Güvenli ve şifreli veri saklama</span>
          </div>
        </div>
      </div>
    </div>

    <!-- ── RIGHT PANEL ── -->
    <div class="right-panel">
      <div class="form-wrapper">

        <!-- Mobile logo -->
        <div class="mobile-logo">
          <div class="mobile-logo-icon">
            <svg width="24" height="24" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
              <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/>
              <circle cx="9" cy="7" r="4"/>
              <path d="M23 21v-2a4 4 0 0 0-3-3.87"/>
              <path d="M16 3.13a4 4 0 0 1 0 7.75"/>
            </svg>
          </div>
          <span class="mobile-logo-text">BUSADER</span>
        </div>

        <!-- Heading -->
        <div class="form-heading">
          <h2>Hoş Geldiniz</h2>
          <p>Hesabınıza giriş yapın</p>
        </div>

        <!-- Session expired alert -->
        <v-alert
          v-if="route.query.expired"
          type="warning"
          variant="tonal"
          density="compact"
          closable
          class="mb-6 rounded-xl"
        >
          Oturumunuz sona erdi. Lütfen tekrar giriş yapın.
        </v-alert>

        <!-- Form -->
        <v-form ref="loginForm" v-model="isFormValid" @submit.prevent="handleLogin">

          <!-- Email -->
          <div class="field-group">
            <label class="field-label">E-posta Adresi</label>
            <v-text-field
              v-model="form.email"
              :rules="[validationRules.required, validationRules.email]"
              type="email"
              variant="outlined"
              density="comfortable"
              placeholder="ornek@email.com"
              prepend-inner-icon="mdi-email-outline"
              hide-details="auto"
              autofocus
              class="custom-field"
            />
          </div>

          <!-- Password -->
          <div class="field-group">
            <label class="field-label">Şifre</label>
            <v-text-field
              v-model="form.password"
              :type="showPassword ? 'text' : 'password'"
              :rules="[validationRules.required]"
              variant="outlined"
              density="comfortable"
              placeholder="••••••••"
              prepend-inner-icon="mdi-lock-outline"
              :append-inner-icon="showPassword ? 'mdi-eye-off-outline' : 'mdi-eye-outline'"
              hide-details="auto"
              class="custom-field"
              @click:append-inner="showPassword = !showPassword"
            />
          </div>

          <!-- Remember + Forgot -->
          <div class="options-row">
            <v-checkbox
              v-model="form.rememberMe"
              label="Beni hatırla"
              density="compact"
              hide-details
              color="primary"
              class="remember-check"
            />
            <button type="button" class="forgot-link">
              Şifremi unuttum
            </button>
          </div>

          <!-- Submit -->
          <v-btn
            type="submit"
            block
            size="large"
            :loading="authStore.isLoading"
            :disabled="!isFormValid"
            class="submit-btn"
          >
            <span class="submit-text">Giriş Yap</span>
            <svg class="submit-arrow" width="18" height="18" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
              <path d="M5 12h14M12 5l7 7-7 7"/>
            </svg>
          </v-btn>

        </v-form>

        <!-- Footer -->
        <p class="form-footer">© {{ new Date().getFullYear() }} BUSADER · Tüm hakları saklıdır</p>
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import type { LoginRequest } from '~/types'

definePageMeta({
  layout: false,
  middleware: 'guest'
})

const route = useRoute()
const authStore = useAuthStore()
const auth = useAuth()
const { validationRules } = useValidators()

const loginForm = ref()
const isFormValid = ref(false)
const showPassword = ref(false)

const form = reactive<LoginRequest>({
  email: '',
  password: '',
  rememberMe: false,
  deviceId: '',
  deviceName: ''
})

const handleLogin = async () => {
  if (!isFormValid.value) return
  try {
    // Save remembered email
    if (form.rememberMe) {
      localStorage.setItem('rememberedEmail', form.email)
    } else {
      localStorage.removeItem('rememberedEmail')
    }
    await auth.login(form)
  } catch (error) {
    console.error('Login failed:', error)
  }
}

onMounted(async () => {
  // Pre-fill from registration redirect
  const registeredEmail = route.query.email as string
  if (registeredEmail) form.email = registeredEmail

  // Remember me pre-fill
  if (process.client) {
    const savedEmail = localStorage.getItem('rememberedEmail')
    if (savedEmail) {
      form.email = savedEmail
      form.rememberMe = true
    }

    // Device info
    let deviceId = localStorage.getItem('deviceId')
    if (!deviceId) {
      deviceId = 'device_' + Date.now() + '_' + Math.random().toString(36).substr(2, 9)
      localStorage.setItem('deviceId', deviceId)
    }
    form.deviceId = deviceId

    const ua = navigator.userAgent
    if (ua.includes('Windows')) form.deviceName = 'Windows Device'
    else if (ua.includes('Mac')) form.deviceName = 'Mac Device'
    else if (ua.includes('Linux')) form.deviceName = 'Linux Device'
    else if (ua.includes('Android')) form.deviceName = 'Android Device'
    else if (ua.includes('iOS')) form.deviceName = 'iOS Device'
    else form.deviceName = 'Unknown Device'
  }
})

useHead({
  title: 'Giriş Yap · BUSADER',
  meta: [{ name: 'description', content: 'BUSADER Dernek Yönetim Sistemine giriş yapın' }]
})
</script>

<style scoped>
/* ─── Root ─────────────────────────────────────────── */
.login-root {
  min-height: 100vh;
  display: flex;
  overflow: hidden;
}

/* ─── Left Panel ────────────────────────────────────── */
.left-panel {
  display: none;
  position: relative;
  width: 50%;
  background: linear-gradient(145deg, #0f172a 0%, #1e293b 40%, #0f172a 100%);
  overflow: hidden;
  flex-direction: column;
  justify-content: center;
}

@media (min-width: 1024px) {
  .left-panel { display: flex; }
  .mobile-logo { display: none; }
}

/* Ambient blobs */
.blob {
  position: absolute;
  border-radius: 50%;
  filter: blur(80px);
  opacity: 0.18;
  pointer-events: none;
}
.blob-1 {
  width: 480px; height: 480px;
  background: #3b82f6;
  top: -120px; left: -120px;
  animation: blobDrift 14s ease-in-out infinite alternate;
}
.blob-2 {
  width: 360px; height: 360px;
  background: #6366f1;
  bottom: -80px; right: -80px;
  animation: blobDrift 18s ease-in-out infinite alternate-reverse;
}
.blob-3 {
  width: 240px; height: 240px;
  background: #0ea5e9;
  top: 50%; left: 50%;
  transform: translate(-50%, -50%);
  animation: blobDrift 10s ease-in-out infinite alternate;
}

@keyframes blobDrift {
  from { transform: translate(0, 0) scale(1); }
  to   { transform: translate(30px, 20px) scale(1.08); }
}

/* Subtle dot-grid overlay */
.grid-overlay {
  position: absolute;
  inset: 0;
  background-image: radial-gradient(circle, rgba(255,255,255,0.06) 1px, transparent 1px);
  background-size: 32px 32px;
  pointer-events: none;
}

/* Content */
.left-content {
  position: relative;
  z-index: 2;
  padding: 56px 52px;
  display: flex;
  flex-direction: column;
  gap: 36px;
}

/* Logo mark */
.logo-mark {
  width: 52px; height: 52px;
  background: rgba(255,255,255,0.08);
  border: 1px solid rgba(255,255,255,0.12);
  border-radius: 14px;
  display: flex; align-items: center; justify-content: center;
  color: white;
  backdrop-filter: blur(8px);
}

/* Text */
.left-badge {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  padding: 5px 14px;
  background: rgba(99,102,241,0.2);
  border: 1px solid rgba(99,102,241,0.35);
  border-radius: 20px;
  color: #a5b4fc;
  font-size: 12px;
  font-weight: 600;
  letter-spacing: 0.5px;
  text-transform: uppercase;
  margin-bottom: 16px;
}

.left-heading {
  font-size: 40px;
  font-weight: 800;
  color: #ffffff;
  line-height: 1.15;
  letter-spacing: -0.5px;
  margin: 0 0 16px 0;
}

.left-desc {
  font-size: 16px;
  color: #94a3b8;
  line-height: 1.7;
  margin: 0;
  max-width: 380px;
}

/* Stats */
.stats-row {
  display: flex;
  align-items: center;
  gap: 24px;
  padding: 20px 24px;
  background: rgba(255,255,255,0.04);
  border: 1px solid rgba(255,255,255,0.08);
  border-radius: 16px;
  backdrop-filter: blur(8px);
}

.stat { text-align: center; }
.stat-value {
  display: block;
  font-size: 22px;
  font-weight: 700;
  color: #ffffff;
  line-height: 1;
  margin-bottom: 4px;
}
.stat-label {
  font-size: 11px;
  color: #64748b;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  font-weight: 500;
}
.stat-divider {
  width: 1px; height: 36px;
  background: rgba(255,255,255,0.1);
  flex-shrink: 0;
}

/* Feature list */
.feature-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}
.feature-item {
  display: flex;
  align-items: center;
  gap: 12px;
  color: #cbd5e1;
  font-size: 14px;
}
.feature-icon {
  width: 28px; height: 28px;
  background: rgba(99,102,241,0.15);
  border-radius: 8px;
  display: flex; align-items: center; justify-content: center;
  color: #818cf8;
  flex-shrink: 0;
}

/* ─── Right Panel ───────────────────────────────────── */
.right-panel {
  width: 100%;
  background: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 40px 24px;
}

@media (min-width: 1024px) {
  .right-panel { width: 50%; }
}

.form-wrapper {
  width: 100%;
  max-width: 420px;
}

/* Mobile logo */
.mobile-logo {
  display: flex;
  align-items: center;
  gap: 10px;
  justify-content: center;
  margin-bottom: 36px;
}

.mobile-logo-icon {
  width: 42px; height: 42px;
  background: #0f172a;
  border-radius: 12px;
  display: flex; align-items: center; justify-content: center;
  color: white;
}

.mobile-logo-text {
  font-size: 18px;
  font-weight: 800;
  color: #0f172a;
  letter-spacing: 0.5px;
}

/* Form heading */
.form-heading {
  margin-bottom: 32px;
}
.form-heading h2 {
  font-size: 28px;
  font-weight: 800;
  color: #0f172a;
  margin: 0 0 6px 0;
  letter-spacing: -0.3px;
}
.form-heading p {
  font-size: 15px;
  color: #64748b;
  margin: 0;
}

/* Fields */
.field-group {
  margin-bottom: 18px;
}
.field-label {
  display: block;
  font-size: 13px;
  font-weight: 600;
  color: #374151;
  margin-bottom: 6px;
  letter-spacing: 0.1px;
}

/* Vuetify field customization */
.custom-field :deep(.v-field) {
  border-radius: 12px !important;
  border-color: #e5e7eb !important;
  background: #f8fafc !important;
  transition: all 0.2s ease;
}
.custom-field :deep(.v-field:hover) {
  border-color: #cbd5e1 !important;
  background: #fff !important;
}
.custom-field :deep(.v-field--focused) {
  border-color: #6366f1 !important;
  background: #fff !important;
  box-shadow: 0 0 0 3px rgba(99,102,241,0.12) !important;
}
.custom-field :deep(.v-field__input) {
  color: #0f172a !important;
  font-size: 15px !important;
}
.custom-field :deep(.v-field__prepend-inner .v-icon) {
  color: #94a3b8 !important;
}

/* Options row */
.options-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 4px 0 24px 0;
}

.remember-check :deep(.v-label) {
  font-size: 13px !important;
  color: #475569 !important;
}

.forgot-link {
  background: none;
  border: none;
  padding: 0;
  font-size: 13px;
  font-weight: 600;
  color: #6366f1;
  cursor: pointer;
  transition: color 0.2s;
}
.forgot-link:hover { color: #4f46e5; }

/* Submit button */
.submit-btn {
  background: linear-gradient(135deg, #1e293b 0%, #0f172a 100%) !important;
  color: white !important;
  border-radius: 12px !important;
  height: 52px !important;
  font-weight: 700 !important;
  text-transform: none !important;
  letter-spacing: 0.2px !important;
  box-shadow: 0 4px 14px rgba(15,23,42,0.25) !important;
  transition: all 0.25s ease !important;
}
.submit-btn:hover:not(:disabled) {
  transform: translateY(-1px) !important;
  box-shadow: 0 8px 20px rgba(15,23,42,0.35) !important;
}
.submit-btn:disabled {
  background: #e2e8f0 !important;
  color: #94a3b8 !important;
  box-shadow: none !important;
}

.submit-text {
  font-size: 15px;
  margin-right: 8px;
}
.submit-arrow {
  transition: transform 0.2s;
}
.submit-btn:hover .submit-arrow {
  transform: translateX(3px);
}

/* Footer */
.form-footer {
  text-align: center;
  font-size: 12px;
  color: #cbd5e1;
  margin-top: 36px;
}

/* Scrollbar */
::-webkit-scrollbar { width: 6px; }
::-webkit-scrollbar-track { background: #f1f5f9; }
::-webkit-scrollbar-thumb { background: #cbd5e1; border-radius: 4px; }
::-webkit-scrollbar-thumb:hover { background: #94a3b8; }
</style>
