# 🏛️ Association Management System

Association Management System, dernekler ve sivil toplum kuruluşları için geliştirilmiş
modern bir **üyelik ve organizasyon yönetim platformudur**.

.NET Web API + Nuxt.js mimarisi ile geliştirilmiştir.

## 📁 Proje Yapısı

```
CivicCore/
├── backend/                 # .NET Web API
│   ├── AssociationMembership.API/
│   ├── AssociationMembership.Application/
│   ├── AssociationMembership.Domain/
│   ├── AssociationMembership.Infrastructure/
│   └── AssociationMembership.sln
├── frontend/               # Nuxt.js Frontend
│   ├── package.json
│   ├── nuxt.config.ts
│   └── ...
└── README.md         
```

## 🎯 Amaç

### Dernek ve organizasyonların;
- Üyelik yönetimini  
- Organizasyon yapısını  
- Yetkilendirme süreçlerini  
- Rol ve izin sistemlerini  
- Alt birim yapılarını
  tek bir sistem üzerinden yönetmesini sağlamak.

## 🧱 Temel Varlıklar (Entities)

- User  
- Tenant  
- TenantGroup  
- Role  
- Permission  
- UserRole  
- RolePermission  
- Membership  
- MembershipTracking  
- RefreshToken  

## 🛠 Geliştirme


### Backend (.NET Web API)
```bash
cd backend
dotnet restore
dotnet run
```

### Frontend (Nuxt.js)
```bash
cd frontend
npm install
npm run dev
```

## ✨ Özellikler

- **🏗️ Clean Architecture** yapısı
- **🔐 JWT Authentication** hazır
- **🗄️ Entity Framework** entegrasyonu
- **⚡ Nuxt.js 3** modern frontend

