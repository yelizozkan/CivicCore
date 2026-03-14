using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByTenant
{
    public class GetMembershipsByTenantQuery : IRequest<Result<List<MembershipListDto>>>
    {
        public int TenantId { get; set; }
        public MembershipStatus? Status { get; set; }
    }
}
