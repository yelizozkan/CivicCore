using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Tenant.Dtos;
using AssociationMembership.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Queries.GetAllTenants
{
    public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, Result<List<TenantDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTenantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<TenantDto>>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _unitOfWork.Tenants.FindAsync(t => !t.IsDeleted);
            var resultDto = _mapper.Map<List<TenantDto>>(tenants);
            return Result<List<TenantDto>>.Success(resultDto);
        }
    }
}
