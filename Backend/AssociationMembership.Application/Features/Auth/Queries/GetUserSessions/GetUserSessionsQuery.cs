using AssociationMembership.Application.DTOs.Auth;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace AssociationMembership.Application.Features.Auth.Queries
{
    public class GetUserSessionsQuery : IRequest<Result<IEnumerable<SessionDto>>>
    {
        public int UserId { get; set; }
    }
} 
