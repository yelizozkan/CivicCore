using AssociationMembership.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; } 
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public string Slug { get; set; }


        public ICollection<TenantGroup> TenantGroups { get; set; }


        public Tenant()
        {
            IsActive = true;
            TenantGroups = new HashSet<TenantGroup>();
        }

    }
}
