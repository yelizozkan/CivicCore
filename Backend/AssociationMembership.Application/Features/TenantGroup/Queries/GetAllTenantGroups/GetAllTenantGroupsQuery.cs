using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.TenantGroup.Queries.GetAllTenantGroups
{
    public class GetAllTenantGroupsQuery : IRequest<Result<List<TenantGroupDto>>>
    {
    }
}
