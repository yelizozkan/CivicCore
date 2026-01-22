using AssociationMembership.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Entities
{
    public class TenantGroup : BaseEntity
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public Tenant Tenant { get; set; }
        public ICollection<Membership> Memberships { get; set; }


        public TenantGroup()
        {
            Memberships = new HashSet<Membership>();
        }

    }
}
