using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByStatus
{
    public class GetMembershipsByStatusQuery : IRequest<Result<List<MembershipListDto>>>
    {
        public MembershipStatus Status { get; set; }
    }
}
