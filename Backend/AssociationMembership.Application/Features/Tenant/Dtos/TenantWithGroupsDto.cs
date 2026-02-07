using AssociationMembership.Application.Features.TenantGroup.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Dtos
{
    public class TenantWithGroupsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TenantGroupDto> TenantGroups { get; set; }
    }
}
