using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace AssociationMembership.Application.Features.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<Result<IEnumerable<RoleDto>>>
    {
    }
} 
