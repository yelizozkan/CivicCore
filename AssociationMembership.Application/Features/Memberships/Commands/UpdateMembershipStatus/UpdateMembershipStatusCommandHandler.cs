using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Application.Services;
using AssociationMembership.Domain.Common.Enums;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Features.Memberships.Commands.UpdateMembershipStatus
{
    public class UpdateMembershipStatusCommandHandler : IRequestHandler<UpdateMembershipStatusCommand, Result<MembershipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMembershipStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<MembershipDto>> Handle(UpdateMembershipStatusCommand request,CancellationToken cancellationToken)
        {

            var membership = await _unitOfWork.Memberships
                .GetMembershipWithTrackingAsync(request.MembershipId);

            if (membership == null || membership.IsDeleted)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Membership not found",404));
            }

            if (membership.Tracking == null)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.InvalidOperation,
                    "Membership tracking not found",400));
            }


            var validationResult = ValidateStatusTransition(membership.Tracking.Status,request.Status);

            if (!validationResult.IsValid)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.InvalidStatusTransition,
                    validationResult.ErrorMessage,400));
            }

   
            membership.Tracking.Status = request.Status;

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                membership.Tracking.Description = request.Description;
            }


            if (request.PaymentAmount.HasValue)
            {
                membership.Tracking.PaymentAmount = request.PaymentAmount.Value;
            }

            if (request.PaymentReceivedDate.HasValue)
            {
                membership.Tracking.PaymentReceivedDate = request.PaymentReceivedDate.Value;
            }


            if (request.Status == MembershipStatus.Approved)
            {
                membership.Tracking.ApprovedDate = DateTime.UtcNow;
            }


            await _unitOfWork.SaveChangesAsync(cancellationToken);


            var resultDto = _mapper.Map<MembershipDto>(membership);
            return Result<MembershipDto>.Success(resultDto);
        }


        private (bool IsValid, string ErrorMessage) ValidateStatusTransition(MembershipStatus currentStatus,MembershipStatus newStatus)
        {

            if (currentStatus == newStatus)
            {
                return (true, null);
            }


            if (currentStatus == MembershipStatus.Pending)
            {
                return (true, null);
            }


            if (currentStatus == MembershipStatus.PreApproved)
            {
                if (newStatus == MembershipStatus.Approved || newStatus == MembershipStatus.Rejected)
                {
                    return (true, null);
                }
                return (false, "PreApproved status can only transition to Approved or Rejected");
            }


            if (currentStatus == MembershipStatus.Approved)
            {
                return (false, "Cannot change status of an approved membership");
            }

            if (currentStatus == MembershipStatus.Rejected)
            {
                return (false, "Cannot change status of a rejected membership");
            }

            return (false, "Invalid status transition");
        }
    }
}
