using AssociationMembership.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Dtos
{
    public class MembershipListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public MembershipStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }

}
