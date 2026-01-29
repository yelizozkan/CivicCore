using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Common.Interfaces.Repositories;
using AssociationMembership.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Features.Memberships.Commands.CreateMembership
{
    public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, Result<MembershipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMembershipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<MembershipDto>> Handle(CreateMembershipCommand request,CancellationToken cancellationToken)
        {
            var dto = request.Membership;


            if (!dto.KvkkAccepted || !dto.ExplicitConsentAccepted)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.ValidationFailed,
                    "All legal consents must be accepted",400));
            }

            var tenantGroupExists = await _unitOfWork.TenantGroups.ExistsAsync(request.TenantGroupId);
            if (!tenantGroupExists)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.NotFound,
                    "TenantGroup not found",404));
            }

            var exists = await _unitOfWork.Memberships
                .IdentityNumberExistsAsync(dto.IdentityNumber, request.TenantGroupId);

            if (exists)
            {
                return Result<MembershipDto>.Failure(Error.Failure(
                    ErrorCode.AlreadyExists,
                    "A membership with this identity number already exists",409));
            }


            var membership = _mapper.Map<Membership>(dto);
            membership.TenantGroupId = request.TenantGroupId;
            membership.ConsentAcceptedDate = DateTime.UtcNow;


            if (membership.BirthDate.Kind == DateTimeKind.Unspecified)
            {
                membership.BirthDate = DateTime.SpecifyKind(membership.BirthDate, DateTimeKind.Utc);
            }


            membership.Tracking = new MembershipTracking
            {
                Status = MembershipStatus.Pending,
                PaymentAmount = 0,
                Description = "Membership application submitted",
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };


            await _unitOfWork.Memberships.AddAsync(membership);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            var savedMembership = await _unitOfWork.Memberships
                .GetMembershipWithTrackingAsync(membership.Id);


            var resultDto = _mapper.Map<MembershipDto>(savedMembership);
            return Result<MembershipDto>.Success(resultDto);
        }
    }
}
