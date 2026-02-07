using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.TenantGroup.Commands.UpdateTenantGroup
{
    public class UpdateTenantGroupCommand : IRequest<Result<TenantGroupDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
