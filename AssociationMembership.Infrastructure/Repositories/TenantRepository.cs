using AssociationMembership.Domain.Common.Interfaces.Repositories;
using AssociationMembership.Domain.Entities;
using AssociationMembership.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CodeExistsAsync(string code)
        {
            return await _context.Set<Tenant>()
                .AnyAsync(t => t.Code == code && !t.IsDeleted);
        }

        public async Task<Tenant?> GetTenantWithGroupsAsync(int tenantId)
        {
            return await _context.Set<Tenant>()
                .Include(t => t.TenantGroups)
                .FirstOrDefaultAsync(t => t.Id == tenantId && !t.IsDeleted);
        }
    }
}
