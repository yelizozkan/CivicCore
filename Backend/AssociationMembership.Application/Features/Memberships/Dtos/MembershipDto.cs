using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Dtos
{
    public class MembershipDto
    {
        public int Id { get; set; }
        public int TenantGroupId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
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
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string ResidenceAddress { get; set; }
        public string WorkplaceAddress { get; set; }


        public string MotivationText { get; set; }


        // Tracking Information
        public MembershipStatus Status { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string TrackingDescription { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
}
