using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Collections.Generic;

namespace AssociationMembership.Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<UserListDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
    }
} 
