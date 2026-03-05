<template>
  <v-dialog
    :model-value="modelValue"
    @update:model-value="$emit('update:modelValue', $event)"
    max-width="900"
    scrollable
  >
    <v-card class="rounded-xl overflow-hidden">
      <!-- Header / Cover Image -->
      <div class="position-relative" style="height: 200px;">
        <v-img
          :src="tenantGroup?.coverImageUrl || 'https://images.unsplash.com/photo-1517457373958-b7bdd4587205?q=80&w=1920&auto=format&fit=crop'"
          cover
          height="100%"
          class="align-end"
          gradient="to top, rgba(0,0,0,0.8), transparent"
        >
          <div class="d-flex align-end px-6 pb-6 gap-4">
            <v-avatar size="100" class="border-lg border-white elevation-4 bg-white">
              <v-img 
                v-if="tenantGroup?.logoUrl" 
                :src="tenantGroup.logoUrl" 
                cover
              ></v-img>
              <span v-else class="text-h3 font-weight-bold text-primary">
                {{ tenantGroup?.name?.charAt(0) }}
              </span>
            </v-avatar>
            <div class="text-white mb-2">
              <h2 class="text-h4 font-weight-bold">{{ tenantGroup?.name }}</h2>
              <div class="d-flex align-center gap-3">
                <v-chip color="white" variant="outlined" size="small">
                  Dernek Platformu
                </v-chip>
                <span v-if="tenantGroup?.foundedDate" class="text-body-2 opacity-80">
                  Kuruluş {{ new Date(tenantGroup.foundedDate).getFullYear() }}
                </span>
              </div>
            </div>
          </div>
        </v-img>
        
        <v-btn
          icon="mdi-close"
          variant="text"
          color="white"
          class="position-absolute top-0 right-0 ma-2"
          @click="$emit('update:modelValue', false)"
        ></v-btn>
      </div>

      <!-- Content -->
      <v-card-text class="pa-0">
        <v-tabs v-model="activeTab" color="primary" align-tabs="start" class="border-b">
          <v-tab value="overview">Genel Bakış</v-tab>
          <v-tab value="contact">İletişim & Bilgi</v-tab>
          <v-tab value="activities" v-if="tenantGroup?.activities?.length">Faaliyetler</v-tab>
        </v-tabs>

        <v-window v-model="activeTab" class="pa-6" style="min-height: 300px;">
          <!-- Overview Tab -->
          <v-window-item value="overview">
            <v-row>
              <v-col cols="12" md="8">
                <div class="mb-6">
                  <h3 class="text-h6 font-weight-bold mb-2 text-primary">Hakkımızda</h3>
                  <p class="text-body-1 text-gray-700">
                    {{ tenantGroup?.description || 'Bu dernek için açıklama bulunmuyor.' }}
                  </p>
                </div>

                <div v-if="tenantGroup?.mission" class="mb-6">
                  <h3 class="text-h6 font-weight-bold mb-2 text-primary">Misyonumuz</h3>
                  <v-card variant="tonal" color="primary" class="pa-4 rounded-lg">
                    <p class="mb-0 font-italic">"{{ tenantGroup.mission }}"</p>
                  </v-card>
                </div>

                <div v-if="tenantGroup?.vision" class="mb-6">
                  <h3 class="text-h6 font-weight-bold mb-2 text-primary">Vizyonumuz</h3>
                  <v-card variant="tonal" color="secondary" class="pa-4 rounded-lg">
                    <p class="mb-0 font-italic">"{{ tenantGroup.vision }}"</p>
                  </v-card>
                </div>
              </v-col>

              <v-col cols="12" md="4">
                <v-card variant="outlined" class="rounded-lg pa-4">
                  <h3 class="text-subtitle-1 font-weight-bold mb-4">Hızlı İstatistikler</h3>
                  <div class="d-flex align-center mb-3">
                    <v-avatar color="primary-lighten-5" size="36" class="mr-3">
                      <v-icon color="primary" size="20">mdi-account-group</v-icon>
                    </v-avatar>
                    <div>
                      <div class="text-h6 font-weight-bold">{{ tenantGroup?.memberCount || '-' }}</div>
                      <div class="text-caption text-gray-500">Aktif Üye</div>
                    </div>
                  </div>
                  <div class="d-flex align-center">
                    <v-avatar color="primary-lighten-5" size="36" class="mr-3">
                      <v-icon color="primary" size="20">mdi-calendar-check</v-icon>
                    </v-avatar>
                    <div>
                      <div class="text-h6 font-weight-bold">
                        {{ tenantGroup?.createdDate ? new Date(tenantGroup.createdDate).getFullYear() : '-' }}
                      </div>
                      <div class="text-caption text-gray-500">Platforma Katılım</div>
                    </div>
                  </div>
                </v-card>
              </v-col>
            </v-row>
          </v-window-item>

          <!-- Contact Tab -->
          <v-window-item value="contact">
            <v-row>
              <v-col cols="12" md="6">
                <v-list class="bg-transparent">
                   <v-list-item class="px-0 mb-4" v-if="tenantGroup?.address">
                    <template v-slot:prepend>
                      <v-avatar color="primary-lighten-5" size="40" class="mr-3">
                        <v-icon color="primary" size="20">mdi-map-marker</v-icon>
                      </v-avatar>
                    </template>
                    <v-list-item-title class="text-caption text-gray-500">Adres</v-list-item-title>
                    <v-list-item-subtitle class="text-body-1 opacity-100 text-wrap">
                      {{ tenantGroup.address }}
                    </v-list-item-subtitle>
                  </v-list-item>

                  <v-list-item class="px-0 mb-4" v-if="tenantGroup?.phone">
                    <template v-slot:prepend>
                      <v-avatar color="primary-lighten-5" size="40" class="mr-3">
                        <v-icon color="primary" size="20">mdi-phone</v-icon>
                      </v-avatar>
                    </template>
                    <v-list-item-title class="text-caption text-gray-500">Telefon</v-list-item-title>
                    <v-list-item-subtitle class="text-body-1 opacity-100">
                      <a :href="`tel:${tenantGroup.phone}`" class="text-decoration-none text-primary font-weight-medium">
                        {{ tenantGroup.phone }}
                      </a>
                    </v-list-item-subtitle>
                  </v-list-item>

                  <v-list-item class="px-0 mb-4" v-if="tenantGroup?.email || tenantGroup?.contactEmail">
                    <template v-slot:prepend>
                      <v-avatar color="primary-lighten-5" size="40" class="mr-3">
                        <v-icon color="primary" size="20">mdi-email</v-icon>
                      </v-avatar>
                    </template>
                    <v-list-item-title class="text-caption text-gray-500">E-posta</v-list-item-title>
                    <v-list-item-subtitle class="text-body-1 opacity-100">
                      <a :href="`mailto:${tenantGroup.email || tenantGroup.contactEmail}`" class="text-decoration-none text-primary font-weight-medium">
                        {{ tenantGroup.email || tenantGroup.contactEmail }}
                      </a>
                    </v-list-item-subtitle>
                  </v-list-item>

                  <v-list-item class="px-0 mb-4" v-if="tenantGroup?.website">
                    <template v-slot:prepend>
                      <v-avatar color="primary-lighten-5" size="40" class="mr-3">
                        <v-icon color="primary" size="20">mdi-web</v-icon>
                      </v-avatar>
                    </template>
                    <v-list-item-title class="text-caption text-gray-500">Web Sitesi</v-list-item-title>
                    <v-list-item-subtitle class="text-body-1 opacity-100">
                      <a :href="tenantGroup.website" target="_blank" class="text-decoration-none text-primary font-weight-medium">
                        {{ tenantGroup.website }}
                      </a>
                      <v-icon size="small" color="primary" class="ml-1">mdi-open-in-new</v-icon>
                    </v-list-item-subtitle>
                  </v-list-item>
                </v-list>
              </v-col>

              <v-col cols="12" md="6">
                <h3 class="text-h6 font-weight-bold mb-4">Bizi Takip Edin</h3>
                <div class="d-flex gap-3 flex-wrap">
                  <v-btn
                    v-if="tenantGroup?.socialMedia?.facebook"
                    :href="tenantGroup.socialMedia.facebook"
                    target="_blank"
                    color="#1877F2"
                    class="text-white"
                    prepend-icon="mdi-facebook"
                  >
                    Facebook
                  </v-btn>
                  <v-btn
                    v-if="tenantGroup?.socialMedia?.twitter"
                    :href="tenantGroup.socialMedia.twitter"
                    target="_blank"
                    color="#1DA1F2"
                    class="text-white"
                    prepend-icon="mdi-twitter"
                  >
                    Twitter
                  </v-btn>
                  <v-btn
                    v-if="tenantGroup?.socialMedia?.instagram"
                    :href="tenantGroup.socialMedia.instagram"
                    target="_blank"
                    color="#E1306C"
                    class="text-white"
                    prepend-icon="mdi-instagram"
                  >
                    Instagram
                  </v-btn>
                  <v-btn
                    v-if="tenantGroup?.socialMedia?.linkedin"
                    :href="tenantGroup.socialMedia.linkedin"
                    target="_blank"
                    color="#0077B5"
                    class="text-white"
                    prepend-icon="mdi-linkedin"
                  >
                    LinkedIn
                  </v-btn>
                  <div v-if="!hasSocialMedia" class="text-body-2 text-gray-500">
                    Sosyal medya profili bulunmuyor.
                  </div>
                </div>
              </v-col>
            </v-row>
          </v-window-item>

          <!-- Activities Tab -->
          <v-window-item value="activities" v-if="tenantGroup?.activities?.length">
            <h3 class="text-h6 font-weight-bold mb-4">Faaliyetlerimiz</h3>
            <v-row>
              <v-col cols="12" md="6" v-for="(activity, index) in tenantGroup.activities" :key="index">
                <v-card variant="outlined" class="d-flex align-center pa-4">
                  <v-icon color="success" class="mr-3">mdi-check-circle-outline</v-icon>
                  <span class="text-body-1">{{ activity }}</span>
                </v-card>
              </v-col>
            </v-row>
          </v-window-item>
        </v-window>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import type { TenantGroup } from '~/types'

const props = defineProps<{
  modelValue: boolean
  tenantGroup: TenantGroup | null | undefined
}>()

defineEmits(['update:modelValue'])

const activeTab = ref('overview')

const hasSocialMedia = computed(() => {
  const sm = props.tenantGroup?.socialMedia
  return sm && (sm.facebook || sm.instagram || sm.twitter || sm.linkedin)
})
</script>

<style scoped>
.v-dialog {
  /* Ensure dialog is scrollable on mobile */
  max-height: 90vh;
}
</style>
