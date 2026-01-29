using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Tenant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Commands.CreateTenant
{
    public class CreateTenantCommand : IRequest<Result<TenantDto>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
    }
}
