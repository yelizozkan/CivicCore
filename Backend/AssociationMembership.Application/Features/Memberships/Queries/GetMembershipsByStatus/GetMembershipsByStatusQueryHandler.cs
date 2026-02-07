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

namespace AssociationMembership.Application.Features.Memberships.Queries.GetMembershipsByStatus
{
    public class GetMembershipsByStatusQueryHandler : IRequestHandler<GetMembershipsByStatusQuery, Result<List<MembershipListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMembershipsByStatusQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<MembershipListDto>>> Handle(GetMembershipsByStatusQuery request, CancellationToken cancellationToken)
        {
            var trackings = await _unitOfWork.MembershipTrackings.GetTrackingsByStatusAsync(request.Status);
            var memberships = trackings.Select(t => t.Membership).ToList();
            var resultDto = _mapper.Map<List<MembershipListDto>>(memberships);
            return Result<List<MembershipListDto>>.Success(resultDto);
        }
    }
}
