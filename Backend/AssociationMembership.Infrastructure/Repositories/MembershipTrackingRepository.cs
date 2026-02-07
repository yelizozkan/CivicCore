using AssociationMembership.Domain.Common.Enums;
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
    public class MembershipTrackingRepository : RepositoryBase<MembershipTracking, int>, IMembershipTrackingRepository
    {
        private readonly ApplicationDbContext _context;

        public MembershipTrackingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<MembershipTracking?> GetByMembershipIdAsync(int membershipId)
        {
            return await _context.Set<MembershipTracking>()
                .Include(t => t.Membership)
                .FirstOrDefaultAsync(t => t.MembershipId == membershipId && !t.IsDeleted);
        }

        public async Task<IEnumerable<MembershipTracking>> GetOverduePaymentsAsync(DateTime today)
        {
            var cutoffDate = today.AddDays(-30);

            return await _context.Set<MembershipTracking>()
                .Include(t => t.Membership)
                .Where(t => !t.IsDeleted &&
                           t.Status == MembershipStatus.PreApproved &&
                           t.PaymentReceivedDate == null &&
                           t.CreatedDate < cutoffDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<MembershipTracking>> GetTrackingsByStatusAsync(MembershipStatus status)
        {
            return await _context.Set<MembershipTracking>()
                .Include(t => t.Membership)
                .Where(t => !t.IsDeleted && t.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<MembershipTracking>> GetWaitingForPaymentAsync()
        {
            return await _context.Set<MembershipTracking>()
                .Include(t => t.Membership)
                .Where(t => !t.IsDeleted &&
                           t.Status == MembershipStatus.PreApproved &&
                           t.PaymentReceivedDate == null)
                .ToListAsync();
        }
    }
}
