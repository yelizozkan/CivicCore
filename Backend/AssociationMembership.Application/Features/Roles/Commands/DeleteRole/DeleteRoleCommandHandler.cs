using AssociationMembership.Application.Features.Roles.Commands;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.DTOs;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Roles.Handlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(request.Id);
            
            if (role == null)
            return Result<RoleDto>.Failure(Error.Failure(
              ErrorCode.NotFound,
              "Role not found"));

        if (role.IsSystemRole)
            return Result<RoleDto>.Failure(Error.Failure(
          ErrorCode.InvalidOperation,
          "Cannot modify system roles"));

        _unitOfWork.Roles.SoftDelete(role);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
} 
