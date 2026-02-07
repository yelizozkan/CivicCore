using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByGroup
{
    public class GetMembershipsByGroupQueryHandler : IRequestHandler<GetMembershipsByGroupQuery, Result<List<MembershipListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMembershipsByGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ;
            _mapper = mapper ;
        }

        public async Task<Result<List<MembershipListDto>>> Handle(GetMembershipsByGroupQuery request, CancellationToken cancellationToken)
        {

            var memberships = await _unitOfWork.Memberships
                .GetMembershipsByGroupAsync(request.TenantGroupId);


            if (request.Status.HasValue)
            {
                memberships = memberships.Where(m => m.Tracking?.Status == request.Status.Value);
            }


            var membershipDtos = _mapper.Map<List<MembershipListDto>>(memberships.ToList());

            return Result<List<MembershipListDto>>.Success(membershipDtos);
        }
    }
}
