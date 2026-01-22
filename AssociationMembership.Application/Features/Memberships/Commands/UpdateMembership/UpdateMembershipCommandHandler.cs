using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Features.Memberships.Commands.UpdateMembership
{
    public class UpdateMembershipCommandHandler : IRequestHandler<UpdateMembershipCommand, Result<MembershipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMembershipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ;
            _mapper = mapper;
        }

        public async Task<Result<MembershipDto>> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken)
        {

            var membership = await _unitOfWork.Memberships
                .GetMembershipWithTrackingAsync(request.Id);

            if (membership == null || membership.IsDeleted)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Membership not found",404));
            }


            if (membership.Tracking?.Status != MembershipStatus.Pending && 
                membership.Tracking?.Status != MembershipStatus.PreApproved)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.InvalidOperation,
                    "Cannot update membership in current status", 400));
            }


            _mapper.Map(request.Membership, membership);


            await _unitOfWork.SaveChangesAsync(cancellationToken);


            var resultDto = _mapper.Map<MembershipDto>(membership);
            return Result<MembershipDto>.Success(resultDto);
        }
    }
}
