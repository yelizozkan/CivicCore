using AssociationMembership.Domain.Common;
using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Entities
{
    public class MembershipTracking : BaseEntity
    {
        public int MembershipId { get; set; }

        public MembershipStatus Status { get; set; }


        public decimal PaymentAmount { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Description { get; set; }

        public Membership Membership { get; set; }

    }
}
