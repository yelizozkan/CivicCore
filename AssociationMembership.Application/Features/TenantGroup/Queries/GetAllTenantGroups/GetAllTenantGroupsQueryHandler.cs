using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
using AssociationMembership.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.TenantGroup.Queries.GetAllTenantGroups
{
    public class GetAllTenantGroupsQueryHandler : IRequestHandler<GetAllTenantGroupsQuery, Result<List<TenantGroupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTenantGroupsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<TenantGroupDto>>> Handle(GetAllTenantGroupsQuery request, CancellationToken cancellationToken)
        {
            var tenantGroups = await _unitOfWork.TenantGroups.FindAsync(g => !g.IsDeleted);
            var resultDto = _mapper.Map<List<TenantGroupDto>>(tenantGroups);
            return Result<List<TenantGroupDto>>.Success(resultDto);
        }
    }
}
