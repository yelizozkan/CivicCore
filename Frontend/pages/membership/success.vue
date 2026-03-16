<template>
  <div class="min-h-screen flex items-center justify-center bg-gradient-to-br from-slate-50 to-slate-100">
    <div class="bg-white rounded-2xl shadow-lg p-8 max-w-md mx-4 text-center">
      
      <!-- Success Icon -->
      <div class="w-20 h-20 bg-emerald-100 rounded-full flex items-center justify-center mx-auto mb-6">
        <svg class="w-10 h-10 text-emerald-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"/>
        </svg>
      </div>
      
      <!-- Title -->
      <h1 class="text-2xl font-bold text-slate-800 mb-3">
        Başvurunuz Alındı!
      </h1>
      
      <!-- Description -->
      <p class="text-slate-600 mb-6 leading-relaxed">
        Üyelik başvurunuz başarıyla alınmıştır. Başvurunuz yönetim kurulu tarafından 
        değerlendirilecek ve sonuç e-posta adresinize bildirilecektir.
      </p>
      
      <!-- Info Box -->
      <div class="bg-blue-50 border border-blue-200 rounded-xl p-4 mb-6">
        <div class="flex items-start gap-3">
          <svg class="w-5 h-5 text-blue-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"/>
          </svg>
          <p class="text-sm text-blue-700 text-left">
            Başvuru süreciniz hakkında bilgi almak için 
            <strong>info@busader.org</strong> adresinden bize ulaşabilirsiniz.
          </p>
        </div>
      </div>
      
      <!-- No redirect button - user stays here or closes the browser -->
      
    </div>
  </div>
</template>

<script setup lang="ts">
// Public page - no auth required
definePageMeta({
  layout: false,
  auth: false
})

useHead({
  title: 'Başvurunuz Alındı - Üyelik',
  meta: [{ name: 'description', content: 'Üyelik başvurunuz başarıyla alındı.' }]
})
</script>
