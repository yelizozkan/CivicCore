<template>
  <!-- Main White Card Container -->
  <div class="bg-white rounded-[20px] shadow-[0_1px_3px_rgba(0,0,0,0.08)] p-6 lg:p-8 lg:px-10 w-full min-h-[calc(100vh-48px)] border border-gray-100 flex flex-col mx-auto">
    <div class="flex-1 w-full max-w-[1600px] mx-auto">
      
      <!-- Loading State -->
      <div v-if="loading" class="d-flex justify-center align-center" style="height: 400px;">
        <v-progress-circular indeterminate color="primary" size="64"></v-progress-circular>
      </div>

      <div v-else class="h-full">
        <!-- Identity / Header Section (Minimalist PrimeStay Style) -->
        <div class="flex justify-between items-center mb-10 pt-4 px-2" v-if="authStore.user?.tenantGroup">
          
          <div class="flex items-center">
            
            <div>
              <h1 class="text-[32px] font-bold text-gray-900 leading-tight">
                {{ authStore.user.tenantGroup.name || 'Dashboard' }}
              </h1>
            </div>
          </div>
          
          <div class="flex items-center gap-4 self-start mt-2">
             <button class="flex items-center gap-2 px-4 py-2 rounded-lg bg-white border border-gray-200 shadow-sm hover:bg-gray-50 transition-colors text-sm font-semibold text-gray-700">
               <v-icon size="18" class="text-gray-500">mdi-export-variant</v-icon>
               İçe Aktar
             </button>
          </div>
          
        </div>
        
        <!-- Quick stats total header -->
        <div class="flex items-center justify-between mt-6 px-2 mb-6">
          <div class="flex items-center gap-3">
            <h2 class="text-lg font-bold text-gray-900">Platform Verileri</h2>
            <span class="text-sm text-gray-500 font-medium">Toplam {{ stats.totalMembers }} kayıt</span>
          </div>
        </div>

        <!-- Stats Overview -->
        <v-row class="mb-6 mx-0 group-stats-row">
          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-blue-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#3b82f6" size="24">mdi-account-multiple</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.totalMembers }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Aktif Üyeler</div>
              </v-card-text>
            </v-card>
          </v-col>
          
          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-amber-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#f59e0b" size="24">mdi-clock-outline</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.pendingCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Bekleyen İnceleme</div>
              </v-card-text>
            </v-card>
          </v-col>

          <v-col cols="12" sm="6" md="3" class="px-2">
             <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-purple-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#8b5cf6" size="24">mdi-cash-clock</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.waitingPaymentCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Ödeme Bekliyor</div>
              </v-card-text>
            </v-card>
          </v-col>

          <v-col cols="12" sm="6" md="3" class="px-2">
            <v-card class="rounded-xl h-100 border border-gray-200 bg-white hover:shadow-md transition-shadow duration-300" elevation="0" style="padding: 24px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-text class="d-flex flex-column align-start p-0">
                <div class="d-flex align-center justify-center rounded-lg bg-red-50 mb-6" style="width: 44px; height: 44px;">
                  <v-icon color="#ef4444" size="24">mdi-alert-circle-outline</v-icon>
                </div>
                <div class="text-[32px] font-bold text-slate-800 leading-none mb-1">{{ stats.overdueCount }}</div>
                <div class="text-sm font-medium text-slate-500 mt-1">Gecikmiş Ödemeler</div>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>

        <!-- Action Required Section -->
        <div class="mb-4" v-if="stats.pendingCount > 0 || stats.waitingPaymentCount > 0 || stats.overdueCount > 0">
          <h2 class="text-[16px] font-bold text-gray-900 mb-3 px-1">Gereken İşlemler</h2>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mb-6">
            
            <div v-if="stats.pendingCount > 0" class="flex items-center justify-between px-4 py-3 bg-gradient-to-r from-amber-50 to-orange-50 border border-amber-200/60 rounded-xl shadow-sm transition-all hover:shadow-md">
              <div class="flex items-center gap-3">
                <div class="w-9 h-9 bg-amber-100 rounded-lg flex items-center justify-center shrink-0">
                  <svg class="w-5 h-5 text-amber-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197m13.5-9a2.5 2.5 0 11-5 0 2.5 2.5 0 015 0z"/>
                  </svg>
                </div>
                <div>
                  <span class="text-sm font-medium text-amber-900">{{ stats.pendingCount }} yeni başvuru</span>
                  <span class="text-sm text-amber-700"> incelemenizi bekliyor</span>
                </div>
              </div>
              <div class="flex items-center gap-2">
                <button @click="navigateTo('/memberships?tab=pending')" class="px-4 py-1.5 text-sm font-medium bg-amber-500 hover:bg-amber-600 text-white rounded-lg transition-colors">
                  İncele
                </button>
                <button class="p-1 text-amber-400 hover:text-amber-600 hover:bg-amber-100 rounded transition-colors hidden sm:block">
                  <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
                  </svg>
                </button>
              </div>
            </div>

            <div v-if="stats.waitingPaymentCount > 0" class="flex items-center justify-between px-4 py-3 bg-gradient-to-r from-blue-50 to-indigo-50 border border-blue-200/60 rounded-xl shadow-sm transition-all hover:shadow-md">
              <div class="flex items-center gap-3">
                <div class="w-9 h-9 bg-blue-100 rounded-lg flex items-center justify-center shrink-0">
                  <svg class="w-5 h-5 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 9V7a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2m2 4h10a2 2 0 002-2v-6a2 2 0 00-2-2H9a2 2 0 00-2 2v6a2 2 0 002 2zm7-5a2 2 0 11-4 0 2 2 0 014 0z"/>
                  </svg>
                </div>
                <div>
                  <span class="text-sm font-medium text-blue-900">{{ stats.waitingPaymentCount }} üye</span>
                  <span class="text-sm text-blue-700"> ödeme bekliyor</span>
                </div>
              </div>
              <div class="flex items-center gap-2">
                <button @click="navigateTo('/memberships?tab=preapproved')" class="px-4 py-1.5 text-sm font-medium bg-blue-500 hover:bg-blue-600 text-white rounded-lg transition-colors">
                  Listeyi Gör
                </button>
                <button class="p-1 text-blue-400 hover:text-blue-600 hover:bg-blue-100 rounded transition-colors hidden sm:block">
                  <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
                  </svg>
                </button>
              </div>
            </div>

            <div v-if="stats.overdueCount > 0" class="flex items-center justify-between px-4 py-3 bg-gradient-to-r from-red-50 to-rose-50 border border-red-200/60 rounded-xl shadow-sm transition-all hover:shadow-md">
              <div class="flex items-center gap-3">
                <div class="w-9 h-9 bg-red-100 rounded-lg flex items-center justify-center shrink-0">
                  <svg class="w-5 h-5 text-red-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"/>
                  </svg>
                </div>
                <div>
                  <span class="text-sm font-medium text-red-900">{{ stats.overdueCount }} üye</span>
                  <span class="text-sm text-red-700"> gecikmiş ödemesi var</span>
                </div>
              </div>
              <div class="flex items-center gap-2">
                <button @click="navigateTo('/memberships?tab=inactive')" class="px-4 py-1.5 text-sm font-medium bg-red-500 hover:bg-red-600 text-white rounded-lg transition-colors">
                  İşlem Yap
                </button>
                <button class="p-1 text-red-400 hover:text-red-600 hover:bg-red-100 rounded transition-colors hidden sm:block">
                  <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
                  </svg>
                </button>
              </div>
            </div>

          </div>
        </div>

        <!-- Recent Applications & Quick Actions -->
        <v-row class="mx-0">
          <v-col cols="12" lg="8" class="px-2">
            <v-card class="rounded-xl border border-gray-200 bg-white" elevation="0" style="box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <v-card-title class="py-4 px-6 border-b border-gray-100">
                <div class="flex items-center justify-between mb-0">
                  <h3 class="text-lg font-semibold text-slate-800">Son Başvurular</h3>
                  <button 
                    @click="showRecentApplicationsModal = true"
                    class="text-sm font-medium text-blue-600 hover:text-blue-700 flex items-center gap-1 transition-colors"
                  >
                    Tümünü Gör
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5l7 7-7 7"/>
                    </svg>
                  </button>
                </div>
              </v-card-title>
              
              <div class="overflow-y-auto max-h-[380px] custom-scrollbar">
                <v-data-table
                  :headers="headers"
                  :items="recentApplications"
                  :loading="loading"
                  hover
                  class="custom-primestay-table"
                  hide-default-footer
                  disable-pagination
                >
                  <template v-slot:item.status="{ item }">
                    <div class="inline-flex items-center px-2.5 py-1 rounded-full text-[13px] font-medium" :class="getBadgeClass(item.status)">
                      <span class="mr-1.5 text-[10px]">●</span>
                      {{ getStatusText(item.status) }}
                    </div>
                  </template>
  
                  <template v-slot:item.createdAt="{ item }">
                    <span class="text-[14px] text-[#374151]">{{ new Date(item.createdAt).toLocaleDateString('tr-TR', { day: 'numeric', month: 'short', year: 'numeric' }) }}</span>
                  </template>
  
                   <template v-slot:item.firstName="{ item }">
                    <div class="flex items-center gap-3">
                      <div class="w-10 h-10 rounded-full flex items-center justify-center text-sm font-semibold text-[#2563eb] bg-[#eff6ff] shrink-0">
                        {{ getInitials(item.fullName || `${item.firstName} ${item.lastName}`) }}
                      </div>
                      <div class="flex flex-col truncate min-w-0">
                        <span class="text-[14px] font-semibold text-[#1e293b] truncate">{{ item.fullName || `${item.firstName} ${item.lastName}` }}</span>
                        <span class="text-[13px] text-[#64748b] truncate">{{ item.email || '-' }}</span>
                      </div>
                    </div>
                  </template>

                <template v-slot:item.phone="{ item }">
                  <span class="text-[14px] text-[#374151]">{{ item.mobilePhone || item.phone || '-' }}</span>
                </template>

                 <template v-slot:item.actions="{ item }">
                  <div class="flex items-center justify-end gap-2">
                    <button class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                      <v-icon size="18">mdi-download-outline</v-icon>
                    </button>
                    <button @click="navigateTo(`/memberships/${item.id}`)" class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                      <v-icon size="18">mdi-eye-outline</v-icon>
                    </button>
                  </div>
                </template>
                
                <template v-slot:no-data>
                  <div class="py-12 flex flex-col items-center justify-center text-center">
                    <div class="w-16 h-16 bg-gray-50 flex items-center justify-center mb-4 border border-gray-100" style="border-radius: 12px;">
                      <v-icon size="36" color="#cbd5e1">mdi-inbox-outline</v-icon>
                    </div>
                    <span class="text-[15px] font-semibold text-slate-800 mb-1">Bekleyen başvuru bulunamadı</span>
                    <span class="text-[13px] text-[#94a3b8] font-medium">Gelen yeni kayıtlar burada listelenecektir.</span>
                  </div>
                </template>
              </v-data-table>
              </div>
            </v-card>
          </v-col>

          <v-col cols="12" lg="4" class="px-2">
             <v-card class="rounded-xl mb-6 border border-gray-200 bg-white" elevation="0" style="padding: 20px; box-shadow: 0 1px 3px rgba(0,0,0,0.05) !important;">
              <h3 class="text-[16px] font-semibold text-slate-800 mb-4 px-1">Hızlı İşlemler</h3>
              
              <div class="flex flex-col gap-2 mb-6">
                
                <button @click="copyRegistrationLink" class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-blue-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-blue-500 transition-colors">mdi-account-plus-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Üye Kayıt Linki</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-blue-500">mdi-content-copy</v-icon>
                </button>

                <button class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-amber-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-amber-500 transition-colors">mdi-file-export-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Üye Listesini Dışa Aktar</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-amber-500">mdi-chevron-right</v-icon>
                </button>

                <button class="group flex items-center justify-between w-full px-4 py-3 rounded-xl bg-white border border-gray-100 shadow-sm hover:shadow-md hover:border-purple-100 hover:-translate-y-0.5 transition-all text-left">
                  <div class="flex items-center gap-3">
                    <div class="flex flex-center shrink-0">
                      <v-icon size="22" color="#64748b" class="group-hover:text-purple-500 transition-colors">mdi-cog-outline</v-icon>
                    </div>
                    <span class="text-[14px] font-semibold text-slate-700 group-hover:text-slate-900">Grup Ayarları</span>
                  </div>
                  <v-icon size="18" color="#94a3b8" class="group-hover:text-purple-500">mdi-chevron-right</v-icon>
                </button>

              </div>
              
              <!-- Helper/Tip Card -->
              <div class="bg-[#f8fafc] rounded-xl border border-dashed border-gray-300 p-4">
                <div class="flex items-start gap-3">
                  <div class="shrink-0 mt-0.5">
                    <v-icon size="20" color="#64748b">mdi-lightbulb-on-outline</v-icon>
                  </div>
                  <div>
                    <span class="text-[12px] font-bold text-slate-700 uppercase tracking-widest block mb-1">İpucu</span>
                    <p class="text-[13px] text-slate-600 font-medium leading-tight m-0">
                      Üye listesinde birden fazla üyeyi seçerek toplu onay verebilirsiniz.
                    </p>
                  </div>
                </div>
              </div>
            </v-card>
          </v-col>
        </v-row>

        <!-- TenantGroup Details Dialog Component -->
        <TenantGroupDetailDialog
          v-model="showGroupDetails"
          :tenant-group="authStore.user?.tenantGroup"
        />

        <!-- SEE ALL MODAL/POPUP -->
        <div v-if="showRecentApplicationsModal" class="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center z-[100]">
          <div class="bg-white rounded-2xl shadow-2xl w-full max-w-4xl max-h-[85vh] flex flex-col mx-4 overflow-hidden">
            <!-- Header -->
            <div class="flex items-center justify-between px-6 py-4 border-b border-slate-100">
              <h2 class="text-xl font-semibold text-slate-800">Son Başvurular</h2>
              <button @click="showRecentApplicationsModal = false" class="p-2 hover:bg-slate-100 rounded-lg transition-colors">
                <svg class="w-5 h-5 text-slate-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
                </svg>
              </button>
            </div>
            
            <!-- Content - Scrollable -->
            <div class="overflow-y-auto flex-1 custom-scrollbar">
               <v-data-table
                  :headers="headers"
                  :items="allPendingApplications"
                  :loading="loading"
                  hover
                  class="custom-primestay-table"
                  hide-default-footer
                  disable-pagination
                >
                  <template v-slot:item.status="{ item }">
                    <div class="inline-flex items-center px-2.5 py-1 rounded-full text-[13px] font-medium" :class="getBadgeClass(item.status)">
                      <span class="mr-1.5 text-[10px]">●</span>
                      {{ getStatusText(item.status) }}
                    </div>
                  </template>
  
                  <template v-slot:item.createdAt="{ item }">
                    <span class="text-[14px] text-[#374151]">{{ new Date(item.createdAt).toLocaleDateString('tr-TR', { day: 'numeric', month: 'short', year: 'numeric' }) }}</span>
                  </template>
  
                   <template v-slot:item.firstName="{ item }">
                    <div class="flex items-center gap-3">
                      <div class="w-10 h-10 rounded-full flex items-center justify-center text-sm font-semibold text-[#2563eb] bg-[#eff6ff] shrink-0">
                        {{ getInitials(item.fullName || `${item.firstName} ${item.lastName}`) }}
                      </div>
                      <div class="flex flex-col truncate min-w-0">
                        <span class="text-[14px] font-semibold text-[#1e293b] truncate">{{ item.fullName || `${item.firstName} ${item.lastName}` }}</span>
                        <span class="text-[13px] text-[#64748b] truncate">{{ item.email || '-' }}</span>
                      </div>
                    </div>
                  </template>
                  <template v-slot:item.phone="{ item }">
                    <span class="text-[14px] text-[#374151]">{{ item.mobilePhone || item.phone || '-' }}</span>
                  </template>
                   <template v-slot:item.actions="{ item }">
                    <div class="flex items-center justify-end gap-2">
                      <button class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                        <v-icon size="18">mdi-download-outline</v-icon>
                      </button>
                      <button @click="navigateTo(`/memberships/${item.id}`)" class="px-3 py-1.5 border border-gray-200 rounded-lg text-gray-600 hover:bg-white hover:shadow-sm hover:border-gray-300 transition-all flex items-center justify-center">
                        <v-icon size="18">mdi-eye-outline</v-icon>
                      </button>
                    </div>
                  </template>
                  <template v-slot:no-data>
                    <div class="py-12 flex flex-col items-center justify-center text-center">
                      <div class="w-16 h-16 bg-gray-50 flex items-center justify-center mb-4 border border-gray-100" style="border-radius: 12px;">
                        <v-icon size="36" color="#cbd5e1">mdi-inbox-outline</v-icon>
                      </div>
                      <span class="text-[15px] font-semibold text-slate-800 mb-1">Bekleyen başvuru bulunamadı</span>
                    </div>
                  </template>
                </v-data-table>
            </div>
            
            <!-- Footer -->
            <div class="flex justify-end px-6 py-4 border-t border-slate-100 bg-slate-50 shrink-0">
              <button 
                @click="navigateTo('/memberships?tab=pending')"
                class="px-4 py-2 bg-slate-800 hover:bg-slate-900 text-white text-sm font-medium rounded-lg transition-colors"
              >
                Tüm Başvuruları Görüntüle
              </button>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '~/stores/auth'
import { useMemberships } from '~/composables/useMemberships'
import TenantGroupDetailDialog from '~/components/TenantGroupDetailDialog.vue'
import { MembershipStatus, type Membership } from '~/types'

const authStore = useAuthStore()
const { getMembershipsByTenant, getMembershipsOverduePayment } = useMemberships()

const showGroupDetails = ref(false)
const loading = ref(true)
const showSnackbar = ref(false)
const snackbarMessage = ref('')

// Data
const recentApplications = ref<Membership[]>([])
const allPendingApplications = ref<Membership[]>([])
const showRecentApplicationsModal = ref(false)
const stats = reactive({
  totalMembers: 0,
  pendingCount: 0,
  waitingPaymentCount: 0,
  overdueCount: 0
})

const headers: any[] = [
  { title: 'BAŞVURU SAHİBİ', key: 'firstName', align: 'start', sortable: false },
  { title: 'TELEFON', key: 'phone', align: 'start', sortable: false },
  { title: 'BAŞVURU TARİHİ', key: 'createdAt', align: 'start', sortable: false },
  { title: 'DURUM', key: 'status', align: 'start', sortable: false },
  { title: 'İŞLEM', key: 'actions', align: 'end', sortable: false },
]

// Custom Badge Styling Logic for Target Component Design
const getInitials = (fullName: string) => {
  if (!fullName) return 'U'
  const names = fullName.trim().split(' ')
  if (names.length >= 2) {
    return (names[0].charAt(0) + names[names.length - 1].charAt(0)).toUpperCase()
  }
  return names[0].charAt(0).toUpperCase()
}

const getBadgeClass = (status: MembershipStatus) => {
  switch (status) {
    case MembershipStatus.Approved: 
      return 'bg-[#dcfce7] text-[#166534]'
    case MembershipStatus.Pending: 
      return 'bg-[#fef3c7] text-[#92400e]'
    case MembershipStatus.Rejected: 
      return 'bg-[#fee2e2] text-[#991b1b]'
    case MembershipStatus.PreApproved: 
      return 'bg-blue-100 text-blue-800'
    case MembershipStatus.Suspended: 
      return 'bg-gray-100 text-gray-800'
    default: 
      return 'bg-gray-100 text-gray-800'
  }
}

const getStatusText = (status: MembershipStatus) => {
   switch (status) {
    case MembershipStatus.Pending: return 'Bekleyen Başvuru'
    case MembershipStatus.PreApproved: return 'Ödeme Bekliyor'
    case MembershipStatus.Approved: return 'Aktif Üye'
    case MembershipStatus.Rejected: return 'Reddedildi'
    case MembershipStatus.Suspended: return 'Askıya Alındı'
    case MembershipStatus.Cancelled: return 'İptal Edildi'
    default: return 'Bilinmiyor'
  }
}

const copyRegistrationLink = async () => {
  const slug = authStore.user?.tenantGroup?.slug
  if (!slug) {
    snackbarMessage.value = 'Grup bilgisi bulunamadı.'
    showSnackbar.value = true
    return
  }
  
  const link = `${window.location.origin}/register/${slug}`
  
  try {
    await navigator.clipboard.writeText(link)
    // Show success message
    snackbarMessage.value = 'Link kopyalandı!'
    showSnackbar.value = true
  } catch (err) {
    console.error('Copy failed:', err)
    snackbarMessage.value = 'Link kopyalanamadı.'
    showSnackbar.value = true
  }
}

// Fetch Data
const fetchDashboardData = async () => {
  let tenantId = authStore.user?.tenantGroup?.tenantId
  
  if (!tenantId && process.client) {
    const savedUser = localStorage.getItem('user')
    if (savedUser) {
      try {
        const parsedUser = JSON.parse(savedUser)
        authStore.user = parsedUser
        tenantId = parsedUser?.tenantGroup?.tenantId
      } catch (e) {
        console.error('Error loading user from localStorage', e)
      }
    }
  }

  if (!tenantId) {
    loading.value = false
    return
  }

  loading.value = true
  try {
    // Parallel API calls with safe fallback array return handling
    const [activeRes, pendingRes, waitingPaymentRes, overdueRes] = await Promise.all([
      getMembershipsByTenant(tenantId, MembershipStatus.Approved).catch(e => { console.error('Error fetching active:', e); return [] }),
      getMembershipsByTenant(tenantId, MembershipStatus.Pending).catch(e => { console.error('Error fetching pending:', e); return [] }),
      getMembershipsByTenant(tenantId, MembershipStatus.PreApproved).catch(e => { console.error('Error fetching waiting payment:', e); return [] }),
      getMembershipsOverduePayment().catch(e => { console.error('Error fetching overdue:', e); return [] })
    ])

    // Extract arrays properly ensuring api compatibility
    const activeMembers = Array.isArray(activeRes) ? activeRes : (activeRes as any)?.data || activeRes || []
    const pendingMembers = Array.isArray(pendingRes) ? pendingRes : (pendingRes as any)?.data || pendingRes || []
    const waitingPayment = Array.isArray(waitingPaymentRes) ? waitingPaymentRes : (waitingPaymentRes as any)?.data || waitingPaymentRes || []
    const overduePayments = Array.isArray(overdueRes) ? overdueRes : (overdueRes as any)?.data || overdueRes || []

    // Update Stats counters based on actual returned elements count
    stats.totalMembers = activeMembers.length
    stats.pendingCount = pendingMembers.length
    stats.waitingPaymentCount = waitingPayment.length
    stats.overdueCount = overduePayments.length

    // Update Recent Applications (Sorting descending and cropping to top 5)
    const sortedPending = [...pendingMembers].sort((a: any, b: any) => {
      const dateA = new Date(a.createdDate || a.createdAt || 0).getTime()
      const dateB = new Date(b.createdDate || b.createdAt || 0).getTime()
      return dateB - dateA
    })
    
    // We seamlessly inject createdAt mapping if API primarily provides createdDate
    allPendingApplications.value = sortedPending.map((m: any) => ({
      ...m,
      createdAt: m.createdDate || m.createdAt || new Date().toISOString()
    }))
    recentApplications.value = allPendingApplications.value.slice(0, 5)

  } catch (error) {
    console.error('Error fetching dashboard data:', error)
  } finally {
    loading.value = false
  }
}

onMounted(async () => {
  if (!authStore.user) {
    await authStore.initializeAuth()
  }
  await fetchDashboardData()
})

// Page metadata
definePageMeta({
  title: 'Dashboard',
  requiresAuth: true
})
</script>

<style scoped>
.container {
  max-width: 1200px;
}

/* Custom Vuetify Table Override to match PrimeStay Minimal Rules */
:deep(.custom-primestay-table) {
  background: white;
}
:deep(.custom-primestay-table .v-table__wrapper > table > thead > tr > th) {
  background-color: #f8fafc !important;
  color: #64748b !important;
  font-size: 12px !important;
  font-weight: 600 !important;
  text-transform: uppercase !important;
  letter-spacing: 0.5px !important;
  padding: 12px 16px !important;
  border-bottom: 1px solid #f1f5f9 !important;
}
:deep(.custom-primestay-table .v-table__wrapper > table > tbody > tr > td) {
  padding: 16px !important;
  border-bottom: 1px solid #f1f5f9 !important;
}
:deep(.custom-primestay-table .v-table__wrapper > table > tbody > tr:hover > td) {
  background-color: #f8fafc !important;
}

/* Subtle Custom Scrollbar for the table wrapper */
.custom-scrollbar::-webkit-scrollbar {
  width: 6px;
}
.custom-scrollbar::-webkit-scrollbar-track {
  background: transparent;
}
.custom-scrollbar::-webkit-scrollbar-thumb {
  background: #e2e8f0;
  border-radius: 4px;
}
.custom-scrollbar::-webkit-scrollbar-thumb:hover {
  background: #cbd5e1;
}
</style>
