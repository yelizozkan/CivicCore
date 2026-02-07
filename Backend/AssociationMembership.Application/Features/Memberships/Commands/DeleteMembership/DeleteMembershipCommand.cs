using AssociationMembership.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.DeleteMembership
{
    public class DeleteMembershipCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
