using AssociationMembership.Domain.Common;
using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace AssociationMembership.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public int TenantGroupId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public string ProfileImageUrl { get; set; } = string.Empty;


        public TenantGroup TenantGroup { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }


        public User()
        {
            UserRoles = new HashSet<UserRole>();
            RefreshTokens = new HashSet<RefreshToken>();
            Status = UserStatus.Active;
            EmailConfirmed = false;
            PhoneConfirmed = false;
        }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 
