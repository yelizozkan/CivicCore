using AssociationMembership.Domain.Common;
using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Entities
{
    public class Membership : BaseAuditableEntity
    {
        public int TenantGroupId { get; set; }

        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string BloodType { get; set; }


        public string EducationLevel { get; set; }
        public string Profession { get; set; }
        public string WorkplaceName { get; set; }
        public string WorkplacePosition { get; set; }


        public string MobilePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string Email { get; set; }
        public string ResidenceAddress { get; set; }
        public string WorkplaceAddress { get; set; }


        public string MotivationText { get; set; }


        public bool KvkkAccepted { get; set; }
        public bool ExplicitConsentAccepted { get; set; }
        public DateTime ConsentAcceptedDate { get; set; }


        public TenantGroup TenantGroup { get; set; }
        public MembershipTracking Tracking { get; set; }


        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
