using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Tenant.Dtos;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<TenantDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTenantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TenantDto>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {

            var codeExists = await _unitOfWork.Tenants.CodeExistsAsync(request.Code);
            if (codeExists)
            {
                return Result<TenantDto>.Failure(Error.Failure(
                    ErrorCode.AlreadyExists,
                    "A tenant with this code already exists", 409));
            }

            var tenant = new Domain.Entities.Tenant
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                LogoUrl = request.LogoUrl,
                IsActive = true
            };

            await _unitOfWork.Tenants.AddAsync(tenant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<TenantDto>(tenant);
            return Result<TenantDto>.Success(resultDto);
        }
    }
}
