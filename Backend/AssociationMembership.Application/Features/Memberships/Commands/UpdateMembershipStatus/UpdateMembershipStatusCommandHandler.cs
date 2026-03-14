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

        public async Task<Result<MembershipDto>> Handle(UpdateMembershipStatusCommand request, CancellationToken cancellationToken)
        {
            var membership = await _unitOfWork.Memberships
                .GetMembershipWithTrackingAsync(request.MembershipId);

            if (membership == null || membership.IsDeleted)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Membership not found", 404));
            }

            if (membership.Tracking == null)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.InvalidOperation,
                    "Membership tracking information not found", 400));
            }

            
            var validationResult = ValidateStatusTransition(
                membership.Tracking.Status,
                request.Status,
                request.PaymentAmount,
                request.PaymentReceivedDate);

            if (!validationResult.IsValid)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.InvalidStatusTransition,
                    validationResult.ErrorMessage, 400));
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


        private (bool IsValid, string ErrorMessage) ValidateStatusTransition(MembershipStatus currentStatus,
        MembershipStatus newStatus,
        decimal? paymentAmount,
        DateTime? paymentReceivedDate)
        {
            
            if (currentStatus == newStatus)
            {
                return (true, null);
            }

            
            if (currentStatus == MembershipStatus.Pending)
            {
                
                if (newStatus == MembershipStatus.PreApproved)
                {
                    return (true, null);
                }
                
                if (newStatus == MembershipStatus.Rejected)
                {
                    return (true, null);
                }
                
                if (newStatus == MembershipStatus.Approved)
                {
                    return (false, "A member must first be moved to the PreApproved status and payment must be received before approval.");
                }
            }

            
            if (currentStatus == MembershipStatus.PreApproved)
            {
                
                if (newStatus == MembershipStatus.Approved)
                {
                    if (!paymentAmount.HasValue || paymentAmount.Value <= 0)
                    {
                        return (false, "Payment amount must be provided to approve the membership.");
                    }
                    if (!paymentReceivedDate.HasValue)
                    {
                        return (false, "Payment received date must be provided to approve the membership.");
                    }
                    return (true, null);
                }
                
                if (newStatus == MembershipStatus.Rejected)
                {
                    return (true, null);
                }
            }

            
            if (currentStatus == MembershipStatus.Approved)
            {
                return (false, "The status of an approved membership cannot be changed.");
            }

            
            if (currentStatus == MembershipStatus.Rejected)
            {
                
                return (false, "The status of a rejected membership cannot be changed.");
            }

            return (false, "Invalid state transition.");
        }
    }
}
