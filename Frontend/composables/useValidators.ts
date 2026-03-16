export const useValidators = () => {
  const validationRules = {
    required: (value: any) => !!value || 'Bu alan zorunludur',

    email: (value: string) => {
      const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
      return pattern.test(value) || 'Geçerli bir e-posta adresi giriniz'
    },

    password: (value: string) => {
      if (!value) return 'Şifre zorunludur'
      if (value.length < 6) return 'Şifre en az 6 karakter olmalıdır'
      if (!/(?=.*[a-z])/.test(value)) return 'Şifre en az bir küçük harf içermelidir'
      if (!/(?=.*[A-Z])/.test(value)) return 'Şifre en az bir büyük harf içermelidir'
      if (!/(?=.*\d)/.test(value)) return 'Şifre en az bir rakam içermelidir'
      return true
    },

    confirmPassword: (password: string) => (value: string) => {
      return value === password || 'Şifreler eşleşmiyor'
    },

    minLength: (min: number) => (value: string) => {
      return (value && value.length >= min) || `En az ${min} karakter giriniz`
    },

    maxLength: (max: number) => (value: string) => {
      return (value && value.length <= max) || `En fazla ${max} karakter giriniz`
    },

    phone: (value: string) => {
      if (!value) return true
      const pattern = /^[\+]?[1-9][\d]{0,15}$/
      return pattern.test(value) || 'Geçerli bir telefon numarası giriniz'
    },

    // TC Kimlik Numarası Algoritma Kontrolü
    tcKimlik: (value: string) => {
      if (!value) return 'TC Kimlik No zorunludur'
      if (!/^\d+$/.test(value)) return 'TC Kimlik No sadece rakam içermelidir'
      if (value.length !== 11) return 'TC Kimlik No 11 haneli olmalıdır'
      if (value[0] === '0') return 'TC Kimlik No 0 ile başlayamaz'

      const digits = value.split('').map(Number)
      const oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8]
      const evenSum = digits[1] + digits[3] + digits[5] + digits[7]
      let digit10 = (oddSum * 7 - evenSum) % 10
      if (digit10 < 0) digit10 += 10
      if (digit10 !== digits[9]) return 'Geçersiz TC Kimlik No'

      const first10Sum = digits.slice(0, 10).reduce((a, b) => a + b, 0)
      const digit11 = first10Sum % 10
      if (digit11 !== digits[10]) return 'Geçersiz TC Kimlik No'

      return true
    },

    // Türkiye Cep Telefonu Kontrolü
    turkishPhone: (value: string) => {
      if (!value) return 'Telefon numarası zorunludur'
      const cleaned = value.replace(/[\s\-\(\)]/g, '')
      const patterns = [
        /^\+90[5][0-9]{9}$/,
        /^0[5][0-9]{9}$/,
        /^[5][0-9]{9}$/
      ]
      const isValid = patterns.some(pattern => pattern.test(cleaned))
      if (!isValid) return 'Geçerli bir cep telefonu numarası giriniz (+90 5XX XXX XX XX)'
      return true
    },

    // Sadece Harf (Türkçe karakterler dahil)
    onlyLetters: (value: string) => {
      if (!value) return true
      const regex = /^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$/
      if (!regex.test(value)) return 'Sadece harf kullanılabilir'
      return true
    }
  }

  return {
    validationRules
  }
}
