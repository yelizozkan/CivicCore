<template>
  <div class="min-h-screen flex items-center justify-center bg-slate-50">
    <div class="text-center p-8">
      <div class="w-16 h-16 bg-amber-100 rounded-full flex items-center justify-center mx-auto mb-4">
        <svg class="w-8 h-8 text-amber-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"/>
        </svg>
      </div>
      <h1 class="text-xl font-semibold text-slate-800 mb-2">Geçersiz Kayıt Linki</h1>
      <p class="text-slate-600">Lütfen size gönderilen kayıt linkini kullanın.</p>
    </div>
  </div>
</template>

<script setup lang="ts">
definePageMeta({ layout: false })
</script>