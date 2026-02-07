using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Features.Memberships.Commands.DeleteMembership
{
    public class DeleteMembershipCommandHandler : IRequestHandler<DeleteMembershipCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMembershipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteMembershipCommand request,CancellationToken cancellationToken)
        {

            var membership = await _unitOfWork.Memberships
                .GetMembershipWithTrackingAsync(request.Id);

            if (membership == null || membership.IsDeleted)
            {
                return Result<bool>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "Membership not found",404));
            }

 
            if (membership.Tracking?.Status != MembershipStatus.Pending &&
                membership.Tracking?.Status != MembershipStatus.Rejected)
            {
                return Result<bool>.Failure(Error.Failure(
                    ErrorCode.InvalidOperation,
                    "Cannot delete membership in current status",400));
            }


            membership.IsDeleted = true;
            if (membership.Tracking != null)
            {
                membership.Tracking.IsDeleted = true;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
