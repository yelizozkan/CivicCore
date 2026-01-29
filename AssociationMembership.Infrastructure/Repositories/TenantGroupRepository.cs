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
    public class TenantGroupRepository :RepositoryBase<TenantGroup, int>, ITenantGroupRepository
    {
        public TenantGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TenantGroup>> GetGroupsByTenantAsync(int tenantId)
        {
            return await _context.Set<TenantGroup>()
                .Where(g => g.TenantId == tenantId && !g.IsDeleted)
                .ToListAsync();
        }
    }
}
