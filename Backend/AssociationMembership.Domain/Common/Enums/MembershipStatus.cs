using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Domain.Common.Enums
{
    public enum MembershipStatus
    {
        Pending = 0,
        PreApproved = 1,    
        Approved = 2,    
        Rejected = 3     
    }
}
