using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.TenantGroup.Commands.DeleteTenantGroup
{
    public class DeleteTenantGroupCommandHandler : IRequestHandler<DeleteTenantGroupCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTenantGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteTenantGroupCommand request, CancellationToken cancellationToken)
        {
            var tenantGroup = await _unitOfWork.TenantGroups.GetByIdAsync(request.Id);
            if (tenantGroup == null || tenantGroup.IsDeleted)
            {
                return Result<bool>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "TenantGroup not found", 404));
            }

            _unitOfWork.TenantGroups.SoftDelete(tenantGroup);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
