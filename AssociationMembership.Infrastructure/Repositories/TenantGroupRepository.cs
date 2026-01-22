using AssociationMembership.Domain.Common.Interfaces.Repositories;
using AssociationMembership.Domain.Entities;
using AssociationMembership.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Infrastructure.Repositories
{
    public class TenantGroupRepository :RepositoryBase<TenantGroup, int>, ITenantGroupRepository
    {
        public TenantGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<TenantGroup>> GetGroupsByTenantAsync(int tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
