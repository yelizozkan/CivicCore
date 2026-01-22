using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.UpdateMembership
{
    public class UpdateMembershipCommand : IRequest<Result<MembershipDto>>
    {
        public int Id { get; set; }
        public UpdateMembershipDto Membership { get; set; }
    }
}
