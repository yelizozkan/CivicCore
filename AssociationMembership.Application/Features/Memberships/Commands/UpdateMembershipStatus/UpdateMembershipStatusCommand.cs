using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.UpdateMembershipStatus
{
    public class UpdateMembershipStatusCommand : IRequest<Result<MembershipDto>>
    {
        public int MembershipId { get; set; }
        public MembershipStatus Status { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public string Description { get; set; }
    }
}
