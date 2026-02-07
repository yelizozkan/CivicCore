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

namespace AssociationMembership.Application.Features.TenantGroup.Commands.UpdateTenantGroup
{
    public class UpdateTenantGroupCommandHandler : IRequestHandler<UpdateTenantGroupCommand, Result<TenantGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTenantGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TenantGroupDto>> Handle(UpdateTenantGroupCommand request, CancellationToken cancellationToken)
        {
            var tenantGroup = await _unitOfWork.TenantGroups.GetByIdAsync(request.Id);
            if (tenantGroup == null || tenantGroup.IsDeleted)
            {
                return Result<TenantGroupDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "TenantGroup not found", 404));
            }


            var existingGroups = await _unitOfWork.TenantGroups.GetGroupsByTenantAsync(tenantGroup.TenantId);
            if (existingGroups.Any(g => g.Id != request.Id && g.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Result<TenantGroupDto>.Failure(Error.Failure(
                    ErrorCode.AlreadyExists,
                    "A group with this name already exists in this tenant", 409));
            }

            tenantGroup.Name = request.Name;
            tenantGroup.Description = request.Description;
            tenantGroup.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.TenantGroups.Update(tenantGroup);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var resultDto = _mapper.Map<TenantGroupDto>(tenantGroup);
            return Result<TenantGroupDto>.Success(resultDto);
        }
    }
}
