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
    public class MembershipRepository: RepositoryBase<Membership, int>, IMembershipRepository
    {
        private readonly ApplicationDbContext _context;

        public MembershipRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Membership?> GetMembershipWithTrackingAsync(int membershipId)
        {
            return await _context.Set<Membership>()
                .Include(m => m.Tracking)
                .FirstOrDefaultAsync(m => m.Id == membershipId);
        }

        public async Task<IEnumerable<Membership>> GetPendingMembershipsByGroupAsync(int tenantGroupId)
        {
            return await _context.Set<Membership>()
                .Include(m => m.Tracking)
                .Where(m => !m.IsDeleted && m.TenantGroupId == tenantGroupId && m.Tracking.Status == MembershipStatus.Pending)
                .ToListAsync();
        }

        public async Task<IEnumerable<Membership>> GetMembershipsByGroupAsync(int tenantGroupId)
        {
            return await _context.Set<Membership>()
                .Include(m => m.Tracking)
                .Where(m => !m.IsDeleted && m.TenantGroupId == tenantGroupId)
                .ToListAsync();
        }

        public async Task<bool> IdentityNumberExistsAsync(string identityNumber, int tenantGroupId)
        {
            return await _context.Set<Membership>()
                .AnyAsync(m => m.IdentityNumber == identityNumber && m.TenantGroupId == tenantGroupId);
        }
    }
}
