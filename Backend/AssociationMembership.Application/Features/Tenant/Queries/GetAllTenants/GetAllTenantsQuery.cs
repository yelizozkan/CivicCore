using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Tenant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Queries.GetAllTenants
{
    public class GetAllTenantsQuery : IRequest<Result<List<TenantDto>>>
    {
    }
}
