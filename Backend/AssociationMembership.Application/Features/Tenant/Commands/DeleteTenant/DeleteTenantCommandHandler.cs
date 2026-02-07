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

namespace AssociationMembership.Application.Features.Tenant.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTenantCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _unitOfWork.Tenants.GetByIdAsync(request.Id);
            if (tenant == null || tenant.IsDeleted)
            {
                return Result<bool>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Tenant not found", 404));
            }

            _unitOfWork.Tenants.SoftDelete(tenant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
