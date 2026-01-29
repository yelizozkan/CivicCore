using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsWaitingPayment
{
    public class GetMembershipsWaitingPaymentQuery : IRequest<Result<List<MembershipListDto>>>
    {
    }
}
