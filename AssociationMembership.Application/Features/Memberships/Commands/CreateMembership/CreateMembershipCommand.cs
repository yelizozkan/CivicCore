using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.CreateMembership
{
    public class CreateMembershipCommand: IRequest<Result<MembershipDto>>
    {
        public int TenantGroupId { get; set; }
        public CreateMembershipDto Membership { get; set; }
    }

}
