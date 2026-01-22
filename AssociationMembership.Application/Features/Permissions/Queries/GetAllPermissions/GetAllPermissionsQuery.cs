using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace AssociationMembership.Application.Features.Permissions.Queries
{
    public class GetAllPermissionsQuery : IRequest<Result<IEnumerable<PermissionDto>>>
    {
    }
} 
