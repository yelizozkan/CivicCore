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

namespace AssociationMembership.Application.Features.TenantGroup.Queries.GetTenantGroupById
{
    public class GetTenantGroupByIdQueryHandler : IRequestHandler<GetTenantGroupByIdQuery, Result<TenantGroupDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTenantGroupByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TenantGroupDto>> Handle(GetTenantGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var tenantGroup = await _unitOfWork.TenantGroups.GetByIdAsync(request.Id);
            if (tenantGroup == null || tenantGroup.IsDeleted)
            {
                return Result<TenantGroupDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "TenantGroup not found", 404));
            }

            var resultDto = _mapper.Map<TenantGroupDto>(tenantGroup);
            return Result<TenantGroupDto>.Success(resultDto);
        }
    }
}
