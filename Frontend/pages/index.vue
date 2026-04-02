<template>
  <div class="login-root">

    <!-- ── LEFT – Dashboard Preview Panel ──────────── -->
    <div class="preview-panel">
      <div class="preview-inner">

        <!-- Heading -->
        <div class="preview-heading">
          <h2>Dernek yönetimini<br>kolaylaştırın.</h2>
          <p>Üyelik, etkinlik ve aidat takibini<br>tek platformdan yönetin.</p>
        </div>

        <!-- Dashboard Mock -->
        <div class="dashboard-mock">
          <div class="mock-window-bar">
            <span class="win-dot red"></span>
            <span class="win-dot yellow"></span>
            <span class="win-dot green"></span>
            <div class="win-title">BUSADER Panel</div>
          </div>

          <div class="mock-body">
            <!-- Mini Sidebar -->
            <div class="mock-sidebar">
              <div class="mock-logo-icon"></div>
              <div class="mock-nav-item active"></div>
              <div class="mock-nav-item"></div>
              <div class="mock-nav-item purple"></div>
              <div class="mock-nav-item"></div>
              <div class="mock-nav-item"></div>
            </div>

            <!-- Content -->
            <div class="mock-content">
              <!-- Top bar -->
              <div class="mock-topbar">
                <div class="mock-page-title"></div>
                <div class="mock-avatar"></div>
              </div>

              <!-- Stat Cards -->
              <div class="mock-cards">
                <div class="mock-card">
                  <div class="card-icon emerald"></div>
                  <div class="card-label short"></div>
                  <div class="card-value"></div>
                </div>
                <div class="mock-card">
                  <div class="card-icon amber"></div>
                  <div class="card-label"></div>
                  <div class="card-value"></div>
                </div>
                <div class="mock-card">
                  <div class="card-icon blue"></div>
                  <div class="card-label short"></div>
                  <div class="card-value wide"></div>
                </div>
              </div>

              <!-- Chart Bar Preview -->
              <div class="mock-chart">
                <div class="chart-bar-row">
                  <div class="chart-bar" style="height:55%"></div>
                  <div class="chart-bar" style="height:75%"></div>
                  <div class="chart-bar" style="height:45%"></div>
                  <div class="chart-bar" style="height:88%"></div>
                  <div class="chart-bar highlight" style="height:65%"></div>
                  <div class="chart-bar" style="height:50%"></div>
                  <div class="chart-bar" style="height:80%"></div>
                </div>
              </div>

              <!-- Table rows -->
              <div class="mock-table">
                <div class="mock-row">
                  <div class="row-avatar purple-av"></div>
                  <div class="row-info">
                    <div class="row-name"></div>
                    <div class="row-sub"></div>
                  </div>
                  <div class="row-badge green-badge"></div>
                </div>
                <div class="mock-row">
                  <div class="row-avatar blue-av"></div>
                  <div class="row-info">
                    <div class="row-name wide"></div>
                    <div class="row-sub"></div>
                  </div>
                  <div class="row-badge amber-badge"></div>
                </div>
                <div class="mock-row">
                  <div class="row-avatar emerald-av"></div>
                  <div class="row-info">
                    <div class="row-name"></div>
                    <div class="row-sub wide"></div>
                  </div>
                  <div class="row-badge green-badge"></div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Feature pills -->
        <div class="feature-pills">
          <div class="pill"><span class="pill-dot emerald-dot"></span>Üye Takibi</div>
          <div class="pill"><span class="pill-dot amber-dot"></span>Aidat Yönetimi</div>
          <div class="pill"><span class="pill-dot purple-dot"></span>Etkinlikler</div>
        </div>

      </div>
    </div>

    <!-- ── RIGHT – Form Panel ─────────────────────── -->
    <div class="form-panel">
      <div class="form-inner">

        <!-- Brand -->
        <div class="brand">
          <div class="brand-icon">
            <svg width="24" height="24" fill="none" stroke="white" stroke-width="2" viewBox="0 0 24 24">
              <path d="M12 2L2 7l10 5 10-5-10-5z"/><path d="M2 17l10 5 10-5"/><path d="M2 12l10 5 10-5"/>
            </svg>
          </div>
          <div>
            <div class="brand-name">BUSADER</div>
            <div class="brand-sub">Federasyonu</div>
          </div>
        </div>

        <!-- Heading -->
        <div class="greeting">
          <h1>Hoş Geldiniz</h1>
          <p>Dernek Yönetim Sistemine giriş yapın</p>
        </div>

        <!-- Session expired -->
        <v-alert
          v-if="route.query.expired"
          type="warning"
          variant="tonal"
          density="compact"
          closable
          class="mb-5 rounded-xl"
        >
          Oturumunuz sona erdi. Lütfen tekrar giriş yapın.
        </v-alert>

        <!-- Form -->
        <v-form ref="loginForm" v-model="isFormValid" @submit.prevent="handleLogin">

          <div class="field-block">
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
              class="vfield"
            />
          </div>

          <div class="field-block">
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
              class="vfield"
              @click:append-inner="showPassword = !showPassword"
            />
          </div>

          <div class="row-options">
            <label class="remember-label">
              <input type="checkbox" v-model="form.rememberMe" class="remember-cb" />
              <span>Beni hatırla</span>
            </label>
            <button type="button" class="forgot-btn">
              <NuxtLink to="/auth/forgot-password" class="forgot-link">Şifremi unuttum</NuxtLink>
            </button>
          </div>

          <button
            type="submit"
            class="submit-btn"
            :class="{ loading: authStore.isLoading, disabled: !isFormValid }"
            :disabled="!isFormValid || authStore.isLoading"
          >
            <span v-if="!authStore.isLoading" class="submit-label">
              Giriş Yap
              <svg width="16" height="16" fill="none" stroke="currentColor" stroke-width="2.5" viewBox="0 0 24 24"><path d="M5 12h14M12 5l7 7-7 7"/></svg>
            </span>
            <span v-else class="submit-label">
              <span class="spinner"></span> Giriş yapılıyor...
            </span>
          </button>

        </v-form>

        <p class="footer-text">© 2026 BUSADER Federasyonu · Tüm hakları saklıdır</p>
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
  email: '', password: '', rememberMe: false, deviceId: '', deviceName: ''
})

// ── Auth ──────────────────────────────────────────────
const handleLogin = async () => {
  if (!isFormValid.value) return
  try {
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
  const registeredEmail = route.query.email as string
  if (registeredEmail) form.email = registeredEmail

  if (process.client) {
    const savedEmail = localStorage.getItem('rememberedEmail')
    if (savedEmail) { form.email = savedEmail; form.rememberMe = true }

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
  title: 'Giriş Yap · BUSADER Federasyonu',
  meta: [{ name: 'description', content: 'BUSADER Federasyonu Dernek Yönetim Sistemine giriş yapın' }]
})
</script>

<style scoped>
/*
  Renk Paleti (BUSADER Kurumsal Mor):
  --purple-900: #4c1d95   en koyu
  --purple-800: #5b21b6
  --purple-700: #6d28d9   orta koyu
  --purple-600: #7c3aed   ana
  --purple-500: #8b5cf6   hover / accent
  --purple-400: #a78bfa   açık accent
  --purple-300: #c4b5fd   çok açık
*/

*, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

.login-root {
  min-height: 100vh;
  display: flex;
  overflow: hidden;
  background: #fafafa;
}

/* ─── LEFT – Preview Panel ──────────────────────── */
.preview-panel {
  display: none;
  position: relative;
  background: linear-gradient(135deg, #4c1d95 0%, #5b21b6 100%);
  overflow: hidden;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
@media (min-width: 1024px) {
  .preview-panel { display: flex; width: 50%; }
}

/* Subtle dot pattern overlay */
.preview-panel::before {
  content: '';
  position: absolute;
  inset: 0;
  background-image: radial-gradient(circle, rgba(255,255,255,0.05) 1px, transparent 1px);
  background-size: 28px 28px;
  pointer-events: none;
}

/* Ambient glow */
.preview-panel::after {
  content: '';
  position: absolute;
  width: 500px; height: 500px;
  background: radial-gradient(circle, rgba(139,92,246,0.35) 0%, transparent 70%);
  top: -100px; left: -100px;
  pointer-events: none;
}

.preview-inner {
  position: relative;
  z-index: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 28px;
  padding: 60px 48px;
  width: 100%;
  max-width: 620px;
}

/* Heading */
.preview-heading { text-align: center; color: white; }
.preview-heading h2 {
  font-size: 32px;
  font-weight: 800;
  line-height: 1.25;
  letter-spacing: -0.5px;
  margin-bottom: 10px;
}
.preview-heading p {
  font-size: 15px;
  color: #c4b5fd;
  line-height: 1.6;
}

/* ── Dashboard Mock ────────────────────────────── */
.dashboard-mock {
  width: 100%;
  background: #ffffff;
  border-radius: 14px;
  box-shadow: 0 20px 60px rgba(0,0,0,0.15), 0 0 0 1px #e2e8f0;
  overflow: hidden;
}

/* Window chrome bar */
.mock-window-bar {
  display: flex;
  align-items: center;
  gap: 6px;
  padding: 10px 14px;
  background: #f8fafc;
  border-bottom: 1px solid #e2e8f0;
}
.win-dot { width: 10px; height: 10px; border-radius: 50%; }
.win-dot.red    { background: #ef4444; opacity: 0.8; }
.win-dot.yellow { background: #f59e0b; opacity: 0.8; }
.win-dot.green  { background: #22c55e; opacity: 0.8; }
.win-title {
  margin-left: 8px;
  font-size: 11px;
  color: #94a3b8;
  font-weight: 500;
  flex: 1;
  text-align: center;
}

/* Body */
.mock-body { display: flex; height: 260px; }

/* Sidebar */
.mock-sidebar {
  width: 52px;
  background: #f1f5f9;
  border-right: 1px solid #e2e8f0;
  padding: 14px 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  flex-shrink: 0;
}
.mock-logo-icon {
  width: 28px; height: 28px;
  background: linear-gradient(135deg, #6d28d9, #8b5cf6);
  border-radius: 7px;
  margin-bottom: 6px;
}
.mock-nav-item {
  width: 32px; height: 5px;
  background: #cbd5e1;
  border-radius: 3px;
}
.mock-nav-item.active { background: #94a3b8; }
.mock-nav-item.purple { background: #7c3aed; }

/* Content area */
.mock-content {
  flex: 1;
  padding: 12px 14px;
  display: flex;
  flex-direction: column;
  gap: 10px;
  overflow: hidden;
}

/* Top bar */
.mock-topbar { display: flex; justify-content: space-between; align-items: center; }
.mock-page-title {
  width: 80px; height: 8px;
  background: #cbd5e1;
  border-radius: 4px;
}
.mock-avatar {
  width: 24px; height: 24px;
  background: linear-gradient(135deg, #7c3aed, #a78bfa);
  border-radius: 50%;
}

/* Stat cards */
.mock-cards {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 7px;
}
.mock-card {
  background: #f8fafc;
  border-radius: 8px;
  padding: 8px;
  border: 1px solid #e2e8f0;
}
.card-icon {
  width: 20px; height: 20px;
  border-radius: 5px;
  margin-bottom: 5px;
}
.card-icon.emerald { background: #d1fae5; }
.card-icon.amber   { background: #fef3c7; }
.card-icon.blue    { background: #ede9fe; }
.card-label {
  height: 4px; width: 28px;
  background: #cbd5e1;
  border-radius: 2px; margin-bottom: 4px;
}
.card-label.short { width: 20px; }
.card-value {
  height: 6px; width: 36px;
  background: #94a3b8;
  border-radius: 3px;
}
.card-value.wide { width: 44px; }

/* Chart */
.mock-chart {
  background: #f8fafc;
  border-radius: 8px;
  padding: 8px 10px;
  border: 1px solid #e2e8f0;
}
.chart-bar-row {
  display: flex;
  align-items: flex-end;
  gap: 5px;
  height: 36px;
}
.chart-bar {
  flex: 1;
  background: #ddd6fe;
  border-radius: 3px 3px 0 0;
}
.chart-bar.highlight { background: #7c3aed; }

/* Table */
.mock-table {
  background: #f8fafc;
  border-radius: 8px;
  padding: 6px 8px;
  display: flex;
  flex-direction: column;
  gap: 6px;
  border: 1px solid #e2e8f0;
}
.mock-row { display: flex; align-items: center; gap: 7px; }
.row-avatar {
  width: 20px; height: 20px;
  border-radius: 50%;
  flex-shrink: 0;
}
.row-avatar.purple-av  { background: linear-gradient(135deg,#6d28d9,#8b5cf6); }
.row-avatar.blue-av    { background: linear-gradient(135deg,#5b21b6,#7c3aed); }
.row-avatar.emerald-av { background: linear-gradient(135deg,#10b981,#34d399); }
.row-info { flex: 1; display: flex; flex-direction: column; gap: 3px; }
.row-name { height: 4px; width: 60px; background: #94a3b8; border-radius: 2px; }
.row-name.wide { width: 80px; }
.row-sub  { height: 3px; width: 44px; background: #cbd5e1; border-radius: 2px; }
.row-sub.wide  { width: 56px; }
.row-badge { width: 36px; height: 14px; border-radius: 4px; flex-shrink: 0; }
.row-badge.green-badge { background: #d1fae5; }
.row-badge.amber-badge { background: #fef3c7; }

/* ── Feature Pills ─────────────────────────────── */
.feature-pills { display: flex; gap: 10px; flex-wrap: wrap; justify-content: center; }
.pill {
  display: flex;
  align-items: center;
  gap: 6px;
  padding: 6px 14px;
  background: rgba(255,255,255,0.12);
  border: 1px solid rgba(255,255,255,0.2);
  border-radius: 20px;
  font-size: 12px;
  font-weight: 500;
  color: #e0d7ff;
}
.pill-dot { width: 7px; height: 7px; border-radius: 50%; }
.pill-dot.emerald-dot { background: #10b981; }
.pill-dot.amber-dot   { background: #f59e0b; }
.pill-dot.purple-dot  { background: #c4b5fd; }

/* ─── RIGHT – Form Panel ────────────────────────── */
.form-panel {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 48px 36px;
  background: #ffffff;
  position: relative;
  z-index: 2;
  border-left: 1px solid #ede9fe;
}
@media (min-width: 1024px) {
  .form-panel { width: 50%; min-height: 100vh; }
}

.form-inner { width: 100%; max-width: 390px; }

/* Brand */
.brand { display: flex; align-items: center; gap: 12px; margin-bottom: 40px; }
.brand-icon {
  width: 46px; height: 46px;
  background: linear-gradient(135deg, #4c1d95, #7c3aed);
  border-radius: 13px;
  display: flex; align-items: center; justify-content: center;
  box-shadow: 0 4px 14px rgba(109,40,217,0.35);
  flex-shrink: 0;
}
.brand-name { font-size: 20px; font-weight: 800; color: #1e1b4b; letter-spacing: 1.5px; line-height: 1; }
.brand-sub  { font-size: 11px; color: #7c3aed; font-weight: 600; letter-spacing: 0.5px; margin-top: 2px; }

/* Greeting */
.greeting { margin-bottom: 30px; }
.greeting h1 { font-size: 28px; font-weight: 800; color: #1e1b4b; margin-bottom: 6px; letter-spacing: -0.3px; }
.greeting p  { font-size: 14px; color: #6b7280; }

.field-block { margin-bottom: 16px; }
.field-label { display: block; font-size: 12.5px; font-weight: 600; color: #374151; margin-bottom: 5px; }

.vfield :deep(.v-field) {
  border-radius: 11px !important;
  border-color: #ede9fe !important;
  background: #faf5ff !important;
  transition: all 0.2s;
}
.vfield :deep(.v-field:hover) { border-color: #a78bfa !important; background: #fff !important; }
.vfield :deep(.v-field--focused) {
  border-color: #8b5cf6 !important;
  background: #fff !important;
  box-shadow: 0 0 0 3px rgba(109,40,217,0.1) !important;
}
.vfield :deep(.v-field__input)             { color: #1e1b4b !important; font-size: 14.5px !important; }
.vfield :deep(.v-field__prepend-inner .v-icon),
.vfield :deep(.v-field__append-inner .v-icon) { color: #7c3aed !important; }

.row-options {
  display: flex; justify-content: space-between; align-items: center;
  margin: 2px 0 22px 0;
}
.remember-label {
  display: flex; align-items: center; gap: 7px;
  font-size: 13px; color: #6b7280; cursor: pointer; user-select: none;
}
.remember-cb { width: 14px; height: 14px; accent-color: #7c3aed; cursor: pointer; }
.forgot-btn {
  background: none; border: none; padding: 0;
  cursor: pointer;
}
.forgot-link {
  font-size: 13px; font-weight: 600; color: #7c3aed;
  text-decoration: none; transition: color 0.2s;
}
.forgot-link:hover { color: #6d28d9; }

.submit-btn {
  width: 100%; height: 50px;
  background: #7c3aed;
  color: white; border: none; border-radius: 12px;
  font-size: 15px; font-weight: 700; cursor: pointer;
  transition: all 0.25s ease;
  box-shadow: 0 6px 20px rgba(109,40,217,0.35);
  letter-spacing: 0.2px;
}
.submit-btn:hover:not(.disabled):not(.loading) {
  background: #6d28d9;
  transform: translateY(-2px);
  box-shadow: 0 10px 28px rgba(109,40,217,0.45);
}
.submit-btn.disabled {
  background: #e5e7eb;
  color: #9ca3af; box-shadow: none; cursor: not-allowed;
}
.submit-label { display: flex; align-items: center; justify-content: center; gap: 8px; }
.spinner {
  width: 16px; height: 16px;
  border: 2px solid rgba(255,255,255,0.3);
  border-top-color: white; border-radius: 50%;
  animation: spin 0.7s linear infinite; display: inline-block;
}
@keyframes spin { to { transform: rotate(360deg); } }

.footer-text {
  margin-top: 32px; font-size: 11px; color: #d1d5db; text-align: center;
}
</style>
