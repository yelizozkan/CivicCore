using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Common.Interfaces.Repositories
{
    public interface ITenantGroupRepository : IRepository<TenantGroup, int>
    {
        Task<IEnumerable<TenantGroup>> GetGroupsByTenantAsync(int tenantId);
    }
}
