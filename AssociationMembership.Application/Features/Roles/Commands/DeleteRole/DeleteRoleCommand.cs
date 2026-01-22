using AssociationMembership.Application.Common.Results;
using MediatR;
using System;

namespace AssociationMembership.Application.Features.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
} 
