using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByGroup
{
    public class GetMembershipsByGroupQuery : IRequest<Result<List<MembershipListDto>>>
    {
        public int TenantGroupId { get; set; }
        public MembershipStatus? Status { get; set; }
    }
}
