using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System;

namespace AssociationMembership.Application.Features.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<Result<RoleDto>>
    {
        public int Id { get; set; }
    }
} 
