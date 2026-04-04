<template>
  <div class="min-h-screen bg-[#f8fafc]">
    <!-- Sidebar Overlay (Mobile Only) -->
    <div 
      v-if="isMobile && isSidebarOpen" 
      @click="toggleSidebar"
      class="fixed inset-0 bg-slate-900 bg-opacity-40 backdrop-blur-sm z-40 transition-opacity duration-300"
    ></div>

    <!-- Sidebar -->
    <aside 
      class="fixed inset-y-0 left-0 bg-white border-r border-slate-200 transform transition-all duration-300 ease-in-out flex flex-col z-50 font-sans shadow-sm" 
      :class="[
        isSidebarOpen ? 'w-[260px] translate-x-0' : (isMobile ? '-translate-x-full w-[260px]' : 'w-[72px] translate-x-0')
      ]"
    >
      <!-- Logo Section -->
      <div 
        class="flex items-center transition-all overflow-hidden whitespace-nowrap h-20" 
        :class="isSidebarOpen ? 'justify-between px-4' : 'justify-center cursor-pointer hover:bg-slate-50'"
        @click="!isSidebarOpen && !isMobile && toggleSidebar()"
      >
        <!-- Sidebar Kapalı -->
        <div v-if="!isSidebarOpen" class="flex justify-center p-2">
          <img src="/images/busaderlogo.png" alt="BUSADER" class="w-12 h-12 object-contain" />
        </div>

        <!-- Sidebar Açık -->
        <div v-else class="flex items-center gap-3">
          <img src="/images/busaderlogo.png" alt="BUSADER" class="w-12 h-12 object-contain" />
          <div class="flex flex-col mt-1">
            <h1 class="font-bold text-slate-800 text-[21px] leading-tight">BUSADER</h1>
            <p class="text-[12px] font-semibold text-purple-600 tracking-wider uppercase">Federasyonu</p>
          </div>
        </div>
        
        <button v-if="isSidebarOpen && !isMobile" @click.stop="toggleSidebar" class="p-1.5 text-slate-400 hover:text-slate-700 hover:bg-slate-100 rounded-lg transition-colors focus:outline-none shrink-0">
          <Icon name="mdi:dock-left" class="h-5 w-5" />
        </button>
      </div>

      <!-- Quick Actions / Search -->
      <div class="px-4 pt-4 pb-2 transition-all overflow-hidden whitespace-nowrap" :class="!isSidebarOpen ? 'px-2' : ''">
        <h3 
          class="text-[11px] font-semibold text-slate-400 uppercase tracking-[0.05em] mb-2 px-1 transition-opacity"
          v-if="isSidebarOpen"
        >
          QUICK ACTIONS
        </h3>
        <div class="relative" v-if="isSidebarOpen">
          <Icon name="mdi:magnify" class="absolute left-3 top-2.5 text-slate-400 h-5 w-5" />
          <input type="text" placeholder="Search" class="w-full bg-slate-50 border border-slate-200 text-sm rounded-lg pl-10 pr-10 py-2.5 text-slate-700 placeholder-slate-400 focus:outline-none focus:ring-1 focus:ring-purple-500 focus:border-purple-500 transition-all">
          <div class="absolute right-2 top-1.5 flex items-center justify-center w-6 h-6 bg-white border border-slate-200 rounded text-[11px] text-slate-500 font-medium shadow-sm">
            ⌘
          </div>
        </div>
        <!-- Collapsed Search Icon Button -->
        <button v-else @click="toggleSidebar" class="w-10 h-10 mx-auto flex items-center justify-center bg-white border border-slate-200 rounded-lg text-slate-500 hover:text-slate-900 hover:shadow-sm hover:bg-slate-50 transition-all focus:outline-none focus:ring-1 focus:ring-purple-500">
          <Icon name="mdi:magnify" class="h-5 w-5" />
        </button>
      </div>
      
      <!-- Navigation Menu -->
      <nav class="flex-1 overflow-y-auto px-3 py-2 space-y-1 scrollbar-hide relative overflow-x-hidden">
        <!-- Dinamik Navigation Items -->
        <template v-for="(item, index) in visibleMenus" :key="index">
          
          <!-- Section Header -->
          <template v-if="item.section">
            <h3 
              v-if="isSidebarOpen"
              class="text-[11px] font-semibold text-slate-400 uppercase tracking-[0.05em] mb-2 px-3 transition-opacity"
              :class="index === 0 ? 'mt-2' : 'mt-6'"
            >
              {{ item.section }}
            </h3>
            <div class="w-6 h-[1px] bg-slate-200 mx-auto transition-all" :class="index === 0 ? 'my-3' : 'mt-6 mb-3'" v-if="!isSidebarOpen"></div>
          </template>

          <!-- Menu Item -->
          <template v-else>
            <v-tooltip :disabled="isSidebarOpen" location="right">
              <template v-slot:activator="{ props }">
                <NuxtLink 
                  v-if="item.to" 
                  :to="item.to" 
                  v-bind="props"
                  class="group relative flex items-center gap-3 py-2.5 transition-all outline-none" 
                  :class="[
                    $route.path === item.to || ($route.path.startsWith(item.to) && item.to !== '/') ? 'bg-purple-50 text-purple-700' : 'text-slate-600 hover:bg-slate-50 hover:text-slate-900',
                    isSidebarOpen ? 'px-3 rounded-lg' : 'justify-center rounded-lg mx-1'
                  ]"
                >
                  <Icon v-if="item.icon" :name="item.icon" class="h-5 w-5 transition-colors shrink-0" :class="[$route.path === item.to || ($route.path.startsWith(item.to) && item.to !== '/') ? 'text-purple-600' : 'text-slate-500 group-hover:text-slate-600', isSidebarOpen ? '' : 'm-0']" />
                  <span v-if="isSidebarOpen" class="text-sm font-medium whitespace-nowrap">{{ item.title }}</span>
                </NuxtLink>
              </template>
              <span>{{ item.title }}</span>
            </v-tooltip>
          </template>
        </template>
      </nav>
      
      <!-- Sidebar Footer (User Profile) -->
      <div class="mt-auto border-t border-slate-200 transition-all duration-300 bg-white" :class="isSidebarOpen ? 'p-4' : 'py-4 flex justify-center'">
        <div class="relative w-full">
          <!-- Dropdown Menu -->
          <div
            v-if="showUserMenu && isSidebarOpen"
            class="absolute bottom-full left-0 mb-2 w-full bg-white rounded-xl shadow-[0_4px_12px_rgba(0,0,0,0.05)] border border-slate-100 z-50 overflow-hidden"
          >
            <div class="py-1">
              <button @click="handleProfileClick" class="flex items-center w-full px-4 py-2.5 text-sm font-medium text-slate-700 hover:bg-slate-50 transition-colors">
                <Icon name="mdi:account-circle-outline" class="mr-3 h-5 w-5 text-slate-400" />
                Profil
              </button>
              <button @click="handleSettingsClick" class="flex items-center w-full px-4 py-2.5 text-sm font-medium text-slate-700 hover:bg-slate-50 transition-colors">
                <Icon name="mdi:cog-outline" class="mr-3 h-5 w-5 text-slate-400" />
                Ayarlar
              </button>
            </div>
          </div>

          <div 
             class="flex items-center gap-3 w-full rounded-xl hover:bg-slate-50 p-2 -mx-2 transition-colors cursor-pointer"
            :class="!isSidebarOpen ? 'justify-center p-0 mx-0 hover:bg-transparent' : 'justify-between'"
            @click="isSidebarOpen ? (showUserMenu = !showUserMenu) : toggleSidebar()"
          >
            <v-tooltip :disabled="isSidebarOpen" location="right">
              <template v-slot:activator="{ props }">
                <div class="flex items-center gap-3 overflow-hidden" v-bind="props">
                  <div class="w-10 h-10 shrink-0 bg-slate-100 border border-slate-200 overflow-hidden rounded-full flex items-center justify-center">
                    <img v-if="userInfo.avatar" :src="userInfo.avatar" class="w-full h-full object-cover">
                    <Icon v-else name="mdi:account" class="w-5 h-5 text-slate-400" />
                  </div>
                  <div v-if="isSidebarOpen" class="flex-1 flex flex-col min-w-0 transition-opacity duration-300">
                    <span class="text-sm font-medium text-slate-800 truncate">{{ userInfo.name || 'System Administrator' }}</span>
                    <span class="text-xs text-slate-500 truncate">{{ userInfo.email || 'admin@busader.org' }}</span>
                  </div>
                </div>
              </template>
              <span>{{ userInfo.name || 'System Administrator' }}</span>
            </v-tooltip>

            <button v-if="isSidebarOpen" @click.stop="logout" class="p-1.5 text-slate-400 hover:text-slate-600 hover:bg-slate-100 rounded-lg transition-colors shrink-0 outline-none">
              <Icon name="mdi:logout" class="h-4 w-4" />
            </button>
          </div>
        </div>
      </div>
    </aside>

    <!-- Main content -->
    <div class="flex flex-col min-h-screen transition-all duration-300" :class="[isSidebarOpen && !isMobile ? 'pl-[260px]' : (isMobile ? 'pl-0' : 'pl-[72px]')]">
      <!-- Mobile Top navbar -->
      <header 
        v-if="isMobile"
        class="sticky top-0 right-0 left-0 bg-white h-16 z-10 transition-all duration-300 border-b border-slate-200" 
      >
        <div class="flex items-center justify-between h-full px-6">
          <div class="flex items-center">
            <!-- Hamburger Menu (Mobile Only) -->
            <button @click="toggleSidebar" class="p-2 -ml-2 text-slate-600 hover:text-slate-900 hover:bg-slate-50 rounded-lg transition-colors focus:outline-none">
              <Icon name="mdi:menu" class="h-6 w-6" />
            </button>
          </div>
        </div>
      </header>

      <!-- Page content -->
      <main class="flex-1 p-4 lg:p-6 lg:pt-6">
        <slot />
      </main>
    </div>
  </div>
</template>

<script setup>
import { navigationItems, filterNavigationByPermissions } from '~/composables/useNavigation'
import { useAuth } from '~/composables/useAuth'
import { useAppData } from '~/composables/useAppData'
import { useAuthStore } from '~/stores/auth'

import { useDisplay } from 'vuetify'

const display = useDisplay()
const isMobile = computed(() => display.smAndDown.value)

const isSidebarOpen = ref(true)
const showUserMenu = ref(false)
const authStore = useAuthStore()
const authUtils = useAuth() // <-- asıl yetki fonksiyonları burada
const router = useRouter()

// App data
const { loadAppData, appData } = useAppData()

// Kullanıcı bilgileri - auth store'dan al
const userInfo = computed(() => ({
  name: authStore.userFullName || 'Kullanıcı',
  email: authStore.user?.email || ''
}))

// Yetkiye göre menüleri filtrele
const visibleMenus = computed(() => {
  return filterNavigationByPermissions(
    navigationItems,
    authUtils.hasPermission,
    authUtils.hasRole
  )
})

const toggleSidebar = () => {
  isSidebarOpen.value = !isSidebarOpen.value
}

const logout = async () => {
  try {
    await authUtils.logout()
    showUserMenu.value = false
  } catch (error) {
    console.error('Çıkış yapılırken bir hata oluştu:', error)
  }
}

// Kullanıcı menüsü işlemleri
const handleProfileClick = () => {
  showUserMenu.value = false
  router.push('/profile')
}

const handleSettingsClick = () => {
  showUserMenu.value = false
  router.push('/settings')
}

// Dışarı tıklandığında menüyü kapat
onMounted(async () => {
  // Restore sidebar state from localStorage or close on mobile
  if (isMobile.value) {
    isSidebarOpen.value = false
  } else {
    const storedState = localStorage.getItem('sidebarOpen')
    if (storedState !== null) {
      isSidebarOpen.value = storedState === 'true'
    }
  }

  // Load app data
  await loadAppData()
  
  document.addEventListener('click', (e) => {
    if (!e.target.closest('.relative')) {
      showUserMenu.value = false
    }
  })
})

watch(isSidebarOpen, (newVal) => {
  localStorage.setItem('sidebarOpen', newVal.toString())
})
</script>


<style scoped>
/* Scrollbar utility classes */
.scrollbar-hide::-webkit-scrollbar {
    display: none;
}
.scrollbar-hide {
    -ms-overflow-style: none;
    scrollbar-width: none;
}
</style>
