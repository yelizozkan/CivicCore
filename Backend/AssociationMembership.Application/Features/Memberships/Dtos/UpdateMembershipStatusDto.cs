using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Dtos
{
    public class UpdateMembershipStatusDto
    {
        public MembershipStatus Status { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public string? Description { get; set; }
    }
}
