  import type { NavigationItem } from '~/types'

  export const navigationItems: NavigationItem[] = [
    { section: 'MENÜ' },
    {
      title: 'Gösterge Paneli',
      icon: 'mdi-view-dashboard-outline',
      to: '/dashboard',
    },
    {
      title: 'Üyeler',
      icon: 'mdi-account-group-outline',
      to: '/memberships'
    },
    {
      title: 'Ödemeler',
      icon: 'mdi-cash-multiple',
      to: '/payments'
    },
    {
      title: 'Etkinlikler',
      icon: 'mdi-calendar-outline',
      to: '/events'
    },
    {
      title: 'Belgeler',
      icon: 'mdi-file-document-outline',
      to: '/documents'
    },
    {
      title: 'Raporlar',
      icon: 'mdi-chart-bar',
      to: '/reports'
    },
    
    // Add existing dynamic routes too, but put them nicely under sections
    // Or maybe since this is a UI prototype update we should just mix them. Let's put Users/Roles/Permissions under DESTEK or below it.
    { section: 'YÖNETİM & DESTEK' },
    {
      title: 'Kullanıcılar',
      icon: 'mdi-account-multiple-outline',
      to: '/users',
      permission: 'Users.Read'
    },
    {
      title: 'Roller',
      icon: 'mdi-shield-account-outline',
      to: '/roles',
      permission: 'Roles.Read'
    },
    {
      title: 'Yetkiler',
      icon: 'mdi-key-outline',
      to: '/permissions',
      permission: 'Permissions.Read'
    },
    {
      title: 'Yardım',
      icon: 'mdi-help-circle-outline',
      to: '/help',
    },
    {
      title: 'Ayarlar',
      icon: 'mdi-cog-outline',
      to: '/settings',
      roles: ['Admin', 'SuperAdmin']
    }
  ]

  export const filterNavigationByPermissions = (
    items: NavigationItem[],
    hasPermission: (permission: string) => boolean,
    hasRole: (role: string) => boolean
  ): NavigationItem[] => {
    return items.filter(item => {
      // Keep section headers
      if (item.section) return true

      // Check permission requirement
      if (item.permission && !hasPermission(item.permission)) {
        return false
      }

      // Check role requirement
      if (item.roles && !item.roles.some((role: string) => hasRole(role))) {
        return false
      }

      // If item has children, filter them recursively
      if (item.children) {
        item.children = filterNavigationByPermissions(item.children, hasPermission, hasRole)
        // Keep parent if it has at least one visible child
        return item.children.length > 0
      }

      return true
    })
  }
