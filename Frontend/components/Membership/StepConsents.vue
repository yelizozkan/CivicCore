<template>
  <div class="step-content">
    <div class="step-header">
      <h2 class="step-title">Yasal Onaylar</h2>
    </div>

    <!-- KVKK Consent -->
    <div class="consent-card" :class="{ 'approved': props.formData.kvkkAccepted, 'border-red-400': errors.kvkkAccepted }">
      <div class="consent-checkbox clickable-area" @click.prevent="toggleConsent('kvkk')">
        <div class="checkbox-wrapper">
          <input type="checkbox" :checked="props.formData.kvkkAccepted" readonly />
          <span class="checkmark"></span>
        </div>
        <span class="checkbox-label">
          <button type="button" class="link-button" @click.stop="openModal('kvkk')">
            KVKK Aydınlatma Metni
          </button>'ni okudum ve kabul ediyorum.
        </span>
      </div>
      <span v-if="props.formData.kvkkAccepted" class="approved-badge">✓ Onaylandı</span>
    </div>
    <div v-if="errors.kvkkAccepted" class="text-red-500 text-sm mt-1 mb-4 ml-1">{{ errors.kvkkAccepted }}</div>

    <!-- Explicit Consent -->
    <div class="consent-card" :class="{ 'approved': props.formData.explicitConsentAccepted, 'border-red-400': errors.explicitConsentAccepted }">
      <div class="consent-checkbox clickable-area" @click.prevent="toggleConsent('explicit')">
        <div class="checkbox-wrapper">
          <input type="checkbox" :checked="props.formData.explicitConsentAccepted" readonly />
          <span class="checkmark"></span>
        </div>
        <span class="checkbox-label">
          <button type="button" class="link-button" @click.stop="openModal('explicit')">
            Açık Rıza Beyanı
          </button>'nı okudum ve kabul ediyorum.
        </span>
      </div>
      <span v-if="props.formData.explicitConsentAccepted" class="approved-badge">✓ Onaylandı</span>
    </div>
    <div v-if="errors.explicitConsentAccepted" class="text-red-500 text-sm mt-1 mb-4 ml-1">{{ errors.explicitConsentAccepted }}</div>

    <div class="info-box">
      <p class="helper-text">Belgeleri okumak için üzerlerine tıklayın. Onay vermek için belgeyi sonuna kadar okumanız gerekmektedir.</p>
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
import { ref, watch } from 'vue'

const props = defineProps<{
  formData: {
    kvkkAccepted: boolean
    explicitConsentAccepted: boolean
    [key: string]: any
  }
}>()

// Modal state
const activeModal = ref<'kvkk' | 'explicit' | null>(null)
const kvkkScrollRef = ref<HTMLElement | null>(null)
const explicitScrollRef = ref<HTMLElement | null>(null)
const kvkkScrolledToBottom = ref(false)
const explicitScrolledToBottom = ref(false)

// Modal functions
const openModal = (type: 'kvkk' | 'explicit') => {
  activeModal.value = type
  if (type === 'kvkk') {
    kvkkScrolledToBottom.value = false
  } else {
    explicitScrolledToBottom.value = false
  }
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
  const isAtBottom = scrollTop + clientHeight >= scrollHeight - 20
  if (type === 'kvkk') {
    kvkkScrolledToBottom.value = isAtBottom
  } else {
    explicitScrolledToBottom.value = isAtBottom
  }
}

const acceptConsent = (type: 'kvkk' | 'explicit') => {
  // Directly mutate props.formData (reactive object from parent ref)
  if (type === 'kvkk') {
    props.formData.kvkkAccepted = true
  } else {
    props.formData.explicitConsentAccepted = true
  }
  console.log('[StepConsents] acceptConsent:', type, 'kvkk:', props.formData.kvkkAccepted, 'explicit:', props.formData.explicitConsentAccepted)
  closeModal()
}

const toggleConsent = (type: 'kvkk' | 'explicit') => {
  if (type === 'kvkk') {
    if (props.formData.kvkkAccepted) {
      props.formData.kvkkAccepted = false
    } else {
      openModal('kvkk')
    }
  } else {
    if (props.formData.explicitConsentAccepted) {
      props.formData.explicitConsentAccepted = false
    } else {
      openModal('explicit')
    }
  }
}

// Validation
const errors = ref<Record<string, string>>({})

const validate = async (): Promise<boolean> => {
  errors.value = {}

  if (!props.formData.kvkkAccepted) {
    errors.value.kvkkAccepted = 'KVKK Aydınlatma Metnini onaylamanız gerekmektedir'
  }
  if (!props.formData.explicitConsentAccepted) {
    errors.value.explicitConsentAccepted = 'Açık Rıza Beyanını onaylamanız gerekmektedir'
  }

  return Object.keys(errors.value).length === 0
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
  font-size: 28px;
  font-weight: 700;
  color: #1e293b;
  margin-bottom: 4px;
}

.step-description {
  font-size: 16px;
  color: #64748b;
}

/* Consent Cards - Light Theme */
.consent-card {
  background-color: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 12px;
  padding: 18px 20px;
  margin-bottom: 12px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  transition: all 0.2s ease;
}

.consent-card:hover {
  border-color: #cbd5e1;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.04);
}

.consent-card.approved {
  background-color: #f8fafc;
  border-color: #94a3b8;
}

.consent-checkbox {
  display: flex;
  align-items: center;
  gap: 14px;
  cursor: pointer;
  flex: 1;
}

.clickable-area {
  cursor: pointer;
}

.checkbox-wrapper {
  position: relative;
}

.consent-checkbox input {
  display: none;
}

.checkmark {
  width: 22px;
  height: 22px;
  border: 2px solid #cbd5e1;
  border-radius: 6px;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.2s ease;
  flex-shrink: 0;
  background-color: #ffffff;
}

.consent-card:hover .checkmark {
  border-color: #94a3b8;
}

.consent-card.approved .checkmark {
  background-color: #22c55e;
  border-color: #22c55e;
}

.consent-card.approved .checkmark::after {
  content: '✓';
  color: white;
  font-size: 13px;
  font-weight: bold;
}

.checkbox-label {
  font-size: 15px;
  color: #334155;
  line-height: 1.4;
}

.link-button {
  background: none;
  border: none;
  color: #3b82f6;
  font-size: 15px;
  font-weight: 600;
  text-decoration: underline;
  text-underline-offset: 2px;
  cursor: pointer;
  padding: 0;
  transition: color 0.2s;
}

.link-button:hover {
  color: #2563eb;
}

.approved-badge {
  font-size: 12px;
  font-weight: 600;
  color: #22c55e;
  background-color: #e2e8f0;
  padding: 6px 12px;
  border-radius: 6px;
  white-space: nowrap;
}

/* Info Box - Nötr */
.info-box {
  display: flex;
  align-items: flex-start;
  gap: 12px;
  padding: 14px 18px;
  background-color: #f8fafc;
  border: 1px solid #e2e8f0;
  border-radius: 10px;
  margin-top: 20px;
}

.info-box span:first-child {
  font-size: 18px;
  line-height: 1;
}

.info-box p {
  font-size: 14px;
  color: #64748b;
  margin: 0;
  line-height: 1.6;
}

/* Modal Styles - Light Theme */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(15, 23, 42, 0.6);
  backdrop-filter: blur(4px);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
  padding: 20px;
}

.modal-container {
  background-color: #ffffff;
  border: 1px solid #e2e8f0;
  border-radius: 16px;
  max-width: 700px;
  width: 100%;
  max-height: 85vh;
  display: flex;
  flex-direction: column;
  box-shadow: 0 25px 50px rgba(0, 0, 0, 0.15);
}

.modal-header {
  padding: 20px 24px;
  border-bottom: 1px solid #e2e8f0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background-color: #f8fafc;
  border-radius: 16px 16px 0 0;
}

.modal-header h2 {
  font-size: 18px;
  font-weight: 700;
  color: #1e293b;
  margin: 0;
}

.modal-close {
  width: 36px;
  height: 36px;
  border: 1px solid #e2e8f0;
  background-color: #ffffff;
  border-radius: 8px;
  font-size: 20px;
  cursor: pointer;
  color: #64748b;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-close:hover {
  background-color: #f1f5f9;
  color: #1e293b;
  border-color: #cbd5e1;
}

.modal-body {
  padding: 24px;
  overflow-y: auto;
  flex: 1;
  background-color: #ffffff;
}

.info-box p:not(.helper-text) {
  font-size: 12px;
  color: #94a3b8;
  text-align: center;
  margin: 8px 0 0 0;
  line-height: 1.4;
}


.document-content {
  color: #334155;
  line-height: 1.7;
}

.document-content h3 {
  font-size: 15px;
  font-weight: 700;
  color: #1e293b;
  margin: 28px 0 12px 0;
  padding-bottom: 8px;
  border-bottom: 1px solid #f1f5f9;
}

.document-content h3:first-child {
  margin-top: 0;
}

.document-content p {
  font-size: 14px;
  margin-bottom: 12px;
  color: #475569;
  text-align: justify;
}

.document-content ul {
  margin: 12px 0;
  padding-left: 20px;
}

.document-content li {
  font-size: 14px;
  margin-bottom: 8px;
  color: #475569;
  line-height: 1.6;
}

.document-content strong {
  color: #1e293b;
  font-weight: 600;
}

.last-update {
  margin-top: 32px;
  padding-top: 16px;
  border-top: 1px solid #e2e8f0;
  font-size: 13px;
  color: #94a3b8;
}

.modal-footer {
  padding: 16px 24px;
  border-top: 1px solid #e2e8f0;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 12px;
  flex-shrink: 0;
  background-color: #f8fafc;
  border-radius: 0 0 16px 16px;
}

.scroll-indicator {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 13px;
  color: #f59e0b;
  font-weight: 500;
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
  background-color: #3b82f6;
  color: white;
  border: none;
  border-radius: 10px;
  font-size: 15px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.accept-button:hover:not(:disabled) {
  background-color: #2563eb;
  box-shadow: 0 4px 12px rgba(37, 99, 235, 0.25);
}

.accept-button:disabled {
  background-color: #e2e8f0;
  color: #94a3b8;
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
    border-radius: 12px;
  }
  
  .modal-header {
    padding: 16px 20px;
    border-radius: 12px 12px 0 0;
  }
  
  .modal-body {
    padding: 20px;
  }
  
  .modal-footer {
    border-radius: 0 0 12px 12px;
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