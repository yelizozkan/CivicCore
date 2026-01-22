using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Common.Interfaces.Repositories
{
    public interface IMembershipTrackingRepository : IRepository<MembershipTracking, int>
    {
        Task<MembershipTracking?> GetByMembershipIdAsync(int membershipId);

        Task<IEnumerable<MembershipTracking>> GetWaitingForPaymentAsync();

        Task<IEnumerable<MembershipTracking>> GetOverduePaymentsAsync(DateTime today);

        Task<IEnumerable<MembershipTracking>> GetTrackingsByStatusAsync(MembershipStatus status);
    }
}
