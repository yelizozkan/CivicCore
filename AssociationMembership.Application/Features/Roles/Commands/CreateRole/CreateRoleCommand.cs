using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace AssociationMembership.Application.Features.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Result<RoleDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
    }
} 
