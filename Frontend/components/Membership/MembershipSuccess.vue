<template>
  <div class="success-container">
    <div class="success-icon">✓</div>
    
    <h2 class="success-title">Başvurunuz Alındı!</h2>
    
    <p class="success-message">
      Üyelik başvurunuz başarıyla tarafımıza ulaştı. Yönetim kurulumuz başvurunuzu en kısa sürede değerlendirecektir.
    </p>
    
    <div class="info-box">
      <span>📧</span>
      <p>Başvuru sonucunuz kayıtlı e-posta adresinize gönderilecektir.</p>
    </div>
    
    <div class="next-steps">
      <h3>Sonraki Adımlar</h3>
      <div class="steps-list">
        <div class="step-item">
          <span class="step-num">1</span>
          <span>Başvurunuz incelenecek</span>
        </div>
        <div class="step-item">
          <span class="step-num">2</span>
          <span>Sonuç e-posta ile bildirilecek</span>
        </div>
        <div class="step-item">
          <span class="step-num">3</span>
          <span>Onay sonrası ödeme bilgileri gönderilecek</span>
        </div>
      </div>
    </div>
    
    <a href="/" class="home-btn">🏠 Ana Sayfaya Dön</a>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.success-container {
  text-align: center;
  padding: 32px 20px;
  animation: fadeIn 0.5s ease;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(20px); }
  to { opacity: 1; transform: translateY(0); }
}

.success-icon {
  width: 80px;
  height: 80px;
  margin: 0 auto 24px;
  background: linear-gradient(135deg, #10b981 0%, #059669 100%);
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 36px;
  color: white;
  box-shadow: 0 0 30px rgba(16, 185, 129, 0.4);
}

.success-title {
  font-size: 24px;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 12px;
}

.success-message {
  font-size: 14px;
  line-height: 1.6;
  color: rgba(255, 255, 255, 0.6);
  max-width: 400px;
  margin: 0 auto 24px;
}

.info-box {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 10px;
  padding: 14px 20px;
  background-color: rgba(59, 130, 246, 0.1);
  border: 1px solid rgba(59, 130, 246, 0.2);
  border-radius: 10px;
  margin-bottom: 24px;
}

.info-box span:first-child {
  font-size: 20px;
}

.info-box p {
  font-size: 13px;
  color: #3b82f6;
  margin: 0;
}

.next-steps {
  background-color: rgba(255, 255, 255, 0.03);
  border: 1px solid rgba(255, 255, 255, 0.08);
  border-radius: 12px;
  padding: 20px;
  margin-bottom: 24px;
  text-align: left;
}

.next-steps h3 {
  font-size: 14px;
  font-weight: 600;
  color: #ffffff;
  margin-bottom: 16px;
  text-align: center;
}

.steps-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.step-item {
  display: flex;
  align-items: center;
  gap: 12px;
}

.step-num {
  width: 28px;
  height: 28px;
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  color: white;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 13px;
  font-weight: 600;
  flex-shrink: 0;
}

.step-item span:last-child {
  font-size: 13px;
  color: rgba(255, 255, 255, 0.6);
}

.home-btn {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  padding: 14px 28px;
  background: linear-gradient(135deg, #3b82f6 0%, #14b8a6 100%);
  color: white;
  text-decoration: none;
  border-radius: 12px;
  font-size: 15px;
  font-weight: 600;
  transition: all 0.2s ease;
  box-shadow: 0 4px 15px rgba(59, 130, 246, 0.3);
}

.home-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(59, 130, 246, 0.4);
}
</style>