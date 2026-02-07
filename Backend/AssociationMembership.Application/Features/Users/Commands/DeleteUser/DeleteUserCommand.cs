using AssociationMembership.Application.Common.Results;
using MediatR;
using System;

namespace AssociationMembership.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
} 
