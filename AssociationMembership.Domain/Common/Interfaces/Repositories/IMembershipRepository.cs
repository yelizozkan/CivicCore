using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Common.Interfaces.Repositories
{
    public interface IMembershipRepository : IRepository<Membership, int>
    {
        Task<Membership?> GetMembershipWithTrackingAsync(int membershipId);

        Task<IEnumerable<Membership>> GetPendingMembershipsByGroupAsync(int tenantGroupId);

        Task<IEnumerable<Membership>> GetMembershipsByGroupAsync(int tenantGroupId);

        Task<bool> IdentityNumberExistsAsync(string identityNumber, int tenantGroupId);
    }
}
