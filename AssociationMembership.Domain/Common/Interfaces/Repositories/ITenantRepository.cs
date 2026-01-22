using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Common.Interfaces.Repositories
{
    public interface ITenantRepository : IRepository<Tenant, int>
    {
        Task<Tenant?> GetTenantWithGroupsAsync(int tenantId);

        Task<bool> CodeExistsAsync(string code);
    }
}
