<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Yasal Onaylar</h2>
      <p class="step-description">Üyelik için gerekli belgeleri okuyup onaylayın</p>
    </div>

    <!-- KVKK Consent -->
    <div class="consent-card" :class="{ 'approved': localData.kvkkAccepted }">
      <label class="consent-checkbox">
        <input type="checkbox" v-model="localData.kvkkAccepted" disabled />
        <span class="checkmark"></span>
        <span class="checkbox-label">
          <button type="button" class="link-button" @click="openModal('kvkk')">
            KVKK Aydınlatma Metni
          </button>'ni okudum ve kabul ediyorum.
        </span>
      </label>
      <span v-if="localData.kvkkAccepted" class="approved-badge">✓ Onaylandı</span>
    </div>

    <!-- Explicit Consent -->
    <div class="consent-card" :class="{ 'approved': localData.explicitConsentAccepted }">
      <label class="consent-checkbox">
        <input type="checkbox" v-model="localData.explicitConsentAccepted" disabled />
        <span class="checkmark"></span>
        <span class="checkbox-label">
          <button type="button" class="link-button" @click="openModal('explicit')">
            Açık Rıza Beyanı
          </button>'nı okudum ve kabul ediyorum.
        </span>
      </label>
      <span v-if="localData.explicitConsentAccepted" class="approved-badge">✓ Onaylandı</span>
    </div>

    <div class="info-box">
      <span>ℹ️</span>
      <p>Belgeleri okumak için üzerlerine tıklayın. Onay vermek için belgeyi sonuna kadar okumanız gerekmektedir.</p>
    </div>

    <!-- KVKK Modal -->
    <Teleport to="body">
      <Transition name="modal">
        <div v-if="activeModal === 'kvkk'" class="modal-overlay" @click.self="closeModal">
          <div class="modal-container">
            <div class="modal-header">
              <h2>KVKK Aydınlatma Metni</h2>
              <button class="modal-close" @click="closeModal">&times;</button>
            </div>
            
            <div class="modal-body" ref="kvkkScrollRef" @scroll="handleScroll('kvkk')">
              <div class="document-content">
                <h3>1. Veri Sorumlusu</h3>
                <p>BUSADER (Büyük Satranç Ailesi Derneği), 6698 sayılı Kişisel Verilerin Korunması Kanunu ("KVKK") kapsamında veri sorumlusu sıfatıyla, kişisel verilerinizi aşağıda açıklanan amaçlar çerçevesinde; hukuka ve dürüstlük kurallarına uygun bir şekilde işleyebilecek, kaydedebilecek, saklayabilecek, sınıflandırabilecek, güncelleyebilecek ve mevzuatın izin verdiği hallerde ve/veya işlendikleri amaçla sınırlı olarak 3. kişilere açıklayabilecek/aktarabilecektir.</p>

                <h3>2. Kişisel Verilerin Toplanması ve İşlenme Amaçları</h3>
                <p>Kişisel verileriniz, derneğimiz tarafından sunulan hizmetlerden sizleri faydalandırmak için gerekli çalışmaların iş birimlerimiz tarafından yapılması, derneğimiz tarafından sunulan hizmetlerin sizlerin beğeni, kullanım alışkanlıkları ve ihtiyaçlarına göre özelleştirilerek sizlere önerilmesi, derneğimizin ve derneğimizle iş ilişkisi içerisinde olan kişilerin hukuki ve ticari güvenliğinin temini amaçlarıyla işlenmektedir.</p>

                <h3>3. İşlenen Kişisel Veriler</h3>
                <p>Üyelik başvurunuz kapsamında aşağıdaki kişisel verileriniz işlenmektedir:</p>
                <ul>
                  <li><strong>Kimlik Bilgileri:</strong> Ad, soyad, T.C. kimlik numarası, doğum tarihi, doğum yeri, anne adı, baba adı</li>
                  <li><strong>İletişim Bilgileri:</strong> E-posta adresi, cep telefonu, iş telefonu, ikamet adresi, iş adresi</li>
                  <li><strong>Mesleki Bilgiler:</strong> Eğitim durumu, meslek, çalışılan kurum, pozisyon</li>
                  <li><strong>Sağlık Bilgileri:</strong> Kan grubu (acil durumlarda kullanılmak üzere)</li>
                </ul>

                <h3>4. Kişisel Verilerin Aktarılması</h3>
                <p>Toplanan kişisel verileriniz; derneğimizin faaliyetlerinin yürütülmesi, hukuki yükümlülüklerimizin yerine getirilmesi ve yasal düzenlemelerin gerektirdiği durumlarda yetkili kamu kurum ve kuruluşlarına aktarılabilecektir. Bunun dışında kişisel verileriniz üçüncü kişilerle paylaşılmamaktadır.</p>

                <h3>5. Kişisel Veri Toplamanın Yöntemi ve Hukuki Sebebi</h3>
                <p>Kişisel verileriniz, üyelik başvuru formunun doldurulması sırasında elektronik ortamda toplanmaktadır. Bu veriler, KVKK'nın 5. ve 6. maddelerinde belirtilen;</p>
                <ul>
                  <li>Açık rızanızın bulunması,</li>
                  <li>Bir sözleşmenin kurulması veya ifasıyla doğrudan doğruya ilgili olması,</li>
                  <li>Veri sorumlusunun hukuki yükümlülüğünü yerine getirebilmesi için zorunlu olması,</li>
                  <li>İlgili kişinin temel hak ve özgürlüklerine zarar vermemek kaydıyla, veri sorumlusunun meşru menfaatleri için veri işlenmesinin zorunlu olması</li>
                </ul>
                <p>hukuki sebeplerine dayanarak işlenmektedir.</p>

                <h3>6. Kişisel Veri Sahibinin Hakları</h3>
                <p>KVKK'nın 11. maddesi uyarınca, kişisel veri sahibi olarak aşağıdaki haklara sahipsiniz:</p>
                <ul>
                  <li>Kişisel verilerinizin işlenip işlenmediğini öğrenme,</li>
                  <li>Kişisel verileriniz işlenmişse buna ilişkin bilgi talep etme,</li>
                  <li>Kişisel verilerinizin işlenme amacını ve bunların amacına uygun kullanılıp kullanılmadığını öğrenme,</li>
                  <li>Yurt içinde veya yurt dışında kişisel verilerinizin aktarıldığı üçüncü kişileri bilme,</li>
                  <li>Kişisel verilerinizin eksik veya yanlış işlenmiş olması halinde bunların düzeltilmesini isteme,</li>
                  <li>KVKK'nın 7. maddesinde öngörülen şartlar çerçevesinde kişisel verilerinizin silinmesini veya yok edilmesini isteme,</li>
                  <li>Kişisel verilerinizin aktarıldığı üçüncü kişilere yukarıda sayılan (e) ve (f) bentleri uyarınca yapılan işlemlerin bildirilmesini isteme,</li>
                  <li>İşlenen verilerinizin münhasıran otomatik sistemler vasıtasıyla analiz edilmesi suretiyle aleyhinize bir sonucun ortaya çıkmasına itiraz etme,</li>
                  <li>Kişisel verilerinizin kanuna aykırı olarak işlenmesi sebebiyle zarara uğramanız halinde zararın giderilmesini talep etme.</li>
                </ul>

                <h3>7. Başvuru Yöntemi</h3>
                <p>Yukarıda belirtilen haklarınızı kullanmak için kimliğinizi tespit edici gerekli bilgiler ile KVKK'nın 11. maddesinde belirtilen haklardan kullanmayı talep ettiğiniz hakkınıza yönelik açıklamalarınızı içeren talebinizi; <strong>info@busader.org</strong> adresine e-posta göndererek veya dernek merkezimize yazılı olarak iletebilirsiniz.</p>

                <h3>8. Değişiklikler</h3>
                <p>İşbu aydınlatma metni, yasal düzenlemeler ve dernek politikalarındaki değişiklikler doğrultusunda güncellenebilir. Güncel metin, derneğimizin web sitesinde yayımlanacaktır.</p>

                <p class="last-update"><em>Son güncelleme: Ocak 2025</em></p>
              </div>
            </div>
            
            <div class="modal-footer">
              <div class="scroll-indicator" v-if="!kvkkScrolledToBottom">
                <span>↓</span> Devam etmek için aşağı kaydırın
              </div>
              <button 
                class="accept-button" 
                :disabled="!kvkkScrolledToBottom"
                @click="acceptConsent('kvkk')"
              >
                Okudum, Kabul Ediyorum
              </button>
            </div>
          </div>
        </div>
      </Transition>
    </Teleport>

    <!-- Explicit Consent Modal -->
    <Teleport to="body">
      <Transition name="modal">
        <div v-if="activeModal === 'explicit'" class="modal-overlay" @click.self="closeModal">
          <div class="modal-container">
            <div class="modal-header">
              <h2>Açık Rıza Beyanı</h2>
              <button class="modal-close" @click="closeModal">&times;</button>
            </div>
            
            <div class="modal-body" ref="explicitScrollRef" @scroll="handleScroll('explicit')">
              <div class="document-content">
                <h3>Açık Rıza Beyanı</h3>
                <p>BUSADER (Büyük Satranç Ailesi Derneği)'ne üyelik başvurusu yapmam sebebiyle, 6698 sayılı Kişisel Verilerin Korunması Kanunu kapsamında, aşağıda belirtilen hususlarda kişisel verilerimin işlenmesine açık rızam ile onay veriyorum:</p>

                <h3>1. Onay Verilen Veri İşleme Faaliyetleri</h3>
                <ul>
                  <li><strong>Kimlik ve İletişim Bilgilerimin Saklanması:</strong> Ad, soyad, T.C. kimlik numarası, doğum tarihi, adres, telefon ve e-posta bilgilerimin dernek üyelik kayıtlarında tutulması ve üyelik süresince saklanması.</li>
                  <li><strong>İletişim Faaliyetleri:</strong> Dernek faaliyetleri, etkinlikler, duyurular ve haberler hakkında e-posta, SMS veya telefon aracılığıyla bilgilendirilmem.</li>
                  <li><strong>Görsel ve İşitsel Kayıtlar:</strong> Dernek etkinliklerinde çekilen fotoğraf ve videoların dernek tanıtımı, sosyal medya paylaşımları ve arşiv amaçlı kullanılması.</li>
                  <li><strong>Yasal Bildirimler:</strong> Üyelik durumum hakkında yasal mercilere (vergi dairesi, bakanlıklar vb.) zorunlu bildirimlerin yapılması.</li>
                  <li><strong>Üçüncü Taraf Paylaşımları:</strong> Derneğin iş birliği yaptığı kurum ve kuruluşlarla (federasyonlar, spor kulüpleri vb.) gerekli bilgilerin paylaşılması.</li>
                </ul>

                <h3>2. Sağlık Verilerine İlişkin Özel Onay</h3>
                <p>Kan grubu bilgimin, acil sağlık durumlarında kullanılmak üzere dernek kayıtlarında tutulmasına ve gerektiğinde sağlık kuruluşlarıyla paylaşılmasına onay veriyorum.</p>

                <h3>3. Verilerimin Korunması</h3>
                <p>Kişisel verilerimin güvenli bir şekilde saklanacağını, yetkisiz erişime karşı korunacağını ve yalnızca belirtilen amaçlar doğrultusunda kullanılacağını kabul ediyorum.</p>

                <h3>4. Onayın Geri Alınması</h3>
                <p>İşbu açık rıza beyanını dilediğim zaman, hiçbir gerekçe göstermeksizin geri alabileceğimi biliyorum. Onayımı geri almak için:</p>
                <ul>
                  <li><strong>info@busader.org</strong> adresine e-posta gönderebilir,</li>
                  <li>Dernek merkezine yazılı başvuruda bulunabilir,</li>
                  <li>Üyelik portalı üzerinden talepte bulunabilirim.</li>
                </ul>
                <p>Onayın geri alınması, geri alma tarihine kadar yapılan veri işleme faaliyetlerinin hukuka uygunluğunu etkilemeyecektir.</p>

                <h3>5. Bilgilendirilme</h3>
                <p>KVKK Aydınlatma Metni'ni okuduğumu, kişisel verilerimin işlenmesine ilişkin haklarım konusunda bilgilendirildiğimi ve yukarıda belirtilen veri işleme faaliyetlerine özgürce, bilgilendirilmiş olarak ve tereddütsüz şekilde onay verdiğimi beyan ederim.</p>

                <p class="last-update"><em>Son güncelleme: Ocak 2025</em></p>
              </div>
            </div>
            
            <div class="modal-footer">
              <div class="scroll-indicator" v-if="!explicitScrolledToBottom">
                <span>↓</span> Devam etmek için aşağı kaydırın
              </div>
              <button 
                class="accept-button" 
                :disabled="!explicitScrolledToBottom"
                @click="acceptConsent('explicit')"
              >
                Okudum, Kabul Ediyorum
              </button>
            </div>
          </div>
        </div>
      </Transition>
    </Teleport>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, watch } from 'vue'

interface FormData {
  kvkkAccepted: boolean
  explicitConsentAccepted: boolean
  [key: string]: any
}

const props = defineProps<{ modelValue: FormData }>()
const emit = defineEmits(['update:modelValue'])

const localData = reactive({ ...props.modelValue })

// Modal state
const activeModal = ref<'kvkk' | 'explicit' | null>(null)
const kvkkScrollRef = ref<HTMLElement | null>(null)
const explicitScrollRef = ref<HTMLElement | null>(null)
const kvkkScrolledToBottom = ref(false)
const explicitScrolledToBottom = ref(false)

// Watch for external changes
watch(() => props.modelValue, (newVal) => {
  Object.assign(localData, newVal)
}, { deep: true })

// Emit changes
watch(localData, (newVal) => {
  emit('update:modelValue', { ...props.modelValue, ...newVal })
}, { deep: true })

// Modal functions
const openModal = (type: 'kvkk' | 'explicit') => {
  activeModal.value = type
  // Reset scroll state when opening
  if (type === 'kvkk') {
    kvkkScrolledToBottom.value = false
  } else {
    explicitScrolledToBottom.value = false
  }
  // Prevent body scroll
  document.body.style.overflow = 'hidden'
}

const closeModal = () => {
  activeModal.value = null
  document.body.style.overflow = ''
}

const handleScroll = (type: 'kvkk' | 'explicit') => {
  const scrollRef = type === 'kvkk' ? kvkkScrollRef.value : explicitScrollRef.value
  if (!scrollRef) return

  const { scrollTop, scrollHeight, clientHeight } = scrollRef
  const isAtBottom = scrollTop + clientHeight >= scrollHeight - 20 // 20px tolerance

  if (type === 'kvkk') {
    kvkkScrolledToBottom.value = isAtBottom
  } else {
    explicitScrolledToBottom.value = isAtBottom
  }
}

const acceptConsent = (type: 'kvkk' | 'explicit') => {
  if (type === 'kvkk') {
    localData.kvkkAccepted = true
  } else {
    localData.explicitConsentAccepted = true
  }
  closeModal()
}

// Validation
const validate = (): boolean => {
  if (!localData.kvkkAccepted) return false
  if (!localData.explicitConsentAccepted) return false
  return true
}

defineExpose({ validate })
</script>

<style scoped>
.step-content {
  animation: fadeIn 0.3s ease;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}

.step-header {
  margin-bottom: 24px;
  text-align: center;
}

.step-title {
  font-size: 22px;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 4px;
}

.step-description {
  font-size: 14px;
  color: rgba(255, 255, 255, 0.5);
}

/* Consent Cards */
.consent-card {
  background-color: rgba(255, 255, 255, 0.03);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 12px;
  padding: 16px;
  margin-bottom: 12px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  transition: all 0.3s ease;
}

.consent-card.approved {
  background-color: rgba(16, 185, 129, 0.1);
  border-color: rgba(16, 185, 129, 0.3);
}

.consent-checkbox {
  display: flex;
  align-items: center;
  gap: 12px;
  cursor: default;
  flex: 1;
}

.consent-checkbox input {
  display: none;
}

.checkmark {
  width: 22px;
  height: 22px;
  border: 2px solid rgba(255, 255, 255, 0.3);
  border-radius: 6px;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.2s ease;
  flex-shrink: 0;
}

.consent-card.approved .checkmark {
  background-color: #10b981;
  border-color: #10b981;
}

.consent-card.approved .checkmark::after {
  content: '✓';
  color: white;
  font-size: 14px;
  font-weight: bold;
}

.checkbox-label {
  font-size: 14px;
  color: rgba(255, 255, 255, 0.8);
}

.link-button {
  background: none;
  border: none;
  color: #3b82f6;
  font-size: 14px;
  font-weight: 600;
  text-decoration: underline;
  cursor: pointer;
  padding: 0;
  transition: color 0.2s;
}

.link-button:hover {
  color: #60a5fa;
}

.approved-badge {
  font-size: 12px;
  font-weight: 600;
  color: #10b981;
  background-color: rgba(16, 185, 129, 0.15);
  padding: 6px 12px;
  border-radius: 20px;
  white-space: nowrap;
}

.info-box {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  padding: 14px 16px;
  background-color: rgba(59, 130, 246, 0.1);
  border: 1px solid rgba(59, 130, 246, 0.2);
  border-radius: 10px;
  margin-top: 16px;
}

.info-box span:first-child {
  font-size: 18px;
}

.info-box p {
  font-size: 13px;
  color: rgba(255, 255, 255, 0.6);
  margin: 0;
  line-height: 1.5;
}

/* Modal Styles */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.85);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
  padding: 20px;
}

.modal-container {
  background-color: #1e293b;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 20px;
  max-width: 700px;
  width: 100%;
  max-height: 85vh;
  display: flex;
  flex-direction: column;
  box-shadow: 0 25px 50px rgba(0, 0, 0, 0.5);
}

.modal-header {
  padding: 20px 24px;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
}

.modal-header h2 {
  font-size: 20px;
  font-weight: 700;
  color: #ffffff;
  margin: 0;
}

.modal-close {
  width: 36px;
  height: 36px;
  border: none;
  background-color: rgba(255, 255, 255, 0.1);
  border-radius: 10px;
  font-size: 24px;
  cursor: pointer;
  color: rgba(255, 255, 255, 0.6);
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-close:hover {
  background-color: rgba(255, 255, 255, 0.15);
  color: #ffffff;
}

.modal-body {
  padding: 24px;
  overflow-y: auto;
  flex: 1;
}

.document-content {
  color: rgba(255, 255, 255, 0.8);
  line-height: 1.7;
}

.document-content h3 {
  font-size: 16px;
  font-weight: 600;
  color: #ffffff;
  margin: 24px 0 12px 0;
}

.document-content h3:first-child {
  margin-top: 0;
}

.document-content p {
  font-size: 14px;
  margin-bottom: 12px;
  color: rgba(255, 255, 255, 0.7);
}

.document-content ul {
  margin: 12px 0;
  padding-left: 24px;
}

.document-content li {
  font-size: 14px;
  margin-bottom: 8px;
  color: rgba(255, 255, 255, 0.7);
}

.document-content strong {
  color: #ffffff;
}

.last-update {
  margin-top: 32px;
  padding-top: 16px;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  font-size: 13px;
  color: rgba(255, 255, 255, 0.4);
}

.modal-footer {
  padding: 16px 24px;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 12px;
  flex-shrink: 0;
}

.scroll-indicator {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: #fbbf24;
  animation: bounce 1s infinite;
}

.scroll-indicator span {
  font-size: 16px;
}

@keyframes bounce {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(4px); }
}

.accept-button {
  width: 100%;
  padding: 14px 24px;
  background: linear-gradient(135deg, #10b981 0%, #059669 100%);
  color: white;
  border: none;
  border-radius: 12px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.accept-button:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 4px 15px rgba(16, 185, 129, 0.4);
}

.accept-button:disabled {
  background: rgba(255, 255, 255, 0.1);
  color: rgba(255, 255, 255, 0.3);
  cursor: not-allowed;
}

/* Modal Transition */
.modal-enter-active,
.modal-leave-active {
  transition: all 0.3s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-from .modal-container,
.modal-leave-to .modal-container {
  transform: scale(0.95) translateY(20px);
}

/* Responsive */
@media (max-width: 640px) {
  .modal-container {
    max-height: 90vh;
    margin: 10px;
  }
  
  .modal-header {
    padding: 16px 20px;
  }
  
  .modal-body {
    padding: 20px;
  }
  
  .consent-card {
    flex-direction: column;
    align-items: flex-start;
    gap: 12px;
  }
  
  .approved-badge {
    align-self: flex-end;
  }
}
</style>