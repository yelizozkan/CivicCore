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
    public class TenantRepository : RepositoryBase<Tenant, int>, ITenantRepository
    {
        public TenantRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Task<bool> CodeExistsAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Tenant?> GetTenantWithGroupsAsync(int tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
