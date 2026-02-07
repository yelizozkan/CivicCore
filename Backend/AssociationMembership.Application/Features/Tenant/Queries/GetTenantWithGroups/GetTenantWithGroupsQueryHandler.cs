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

namespace AssociationMembership.Application.Features.Tenant.Queries.GetTenantWithGroups
{
    public class GetTenantWithGroupsQueryHandler : IRequestHandler<GetTenantWithGroupsQuery, Result<TenantWithGroupsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTenantWithGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TenantWithGroupsDto>> Handle(GetTenantWithGroupsQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _unitOfWork.Tenants.GetTenantWithGroupsAsync(request.Id);
            if (tenant == null || tenant.IsDeleted)
            {
                return Result<TenantWithGroupsDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Tenant not found", 404));
            }

            var resultDto = _mapper.Map<TenantWithGroupsDto>(tenant);
            return Result<TenantWithGroupsDto>.Success(resultDto);
        }
    }
}
