using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
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

namespace AssociationMembership.Application.Features.TenantGroup.Commands.CreateTenantGroup
{
    public class CreateTenantGroupCommandHandler : IRequestHandler<CreateTenantGroupCommand, Result<TenantGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTenantGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TenantGroupDto>> Handle(CreateTenantGroupCommand request, CancellationToken cancellationToken)
        {

            var tenant = await _unitOfWork.Tenants.GetByIdAsync(request.TenantId);
            if (tenant == null || tenant.IsDeleted)
            {
                return Result<TenantGroupDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Tenant not found", 404));
            }


            var existingGroups = await _unitOfWork.TenantGroups.GetGroupsByTenantAsync(request.TenantId);
            if (existingGroups.Any(g => g.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Result<TenantGroupDto>.Failure(Error.Failure(
                    ErrorCode.AlreadyExists,
                    "A group with this name already exists in this tenant", 409));
            }

            var tenantGroup = new Domain.Entities.TenantGroup
            {
                TenantId = request.TenantId,
                Name = request.Name,
                Description = request.Description
            };

            await _unitOfWork.TenantGroups.AddAsync(tenantGroup);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<TenantGroupDto>(tenantGroup);
            return Result<TenantGroupDto>.Success(resultDto);
        }
    }
}
