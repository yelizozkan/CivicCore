using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.UpdateMembershipStatus
{
    public class UpdateMembershipStatusCommandValidator : AbstractValidator<UpdateMembershipStatusCommand>
    {
        public UpdateMembershipStatusCommandValidator()
        {
            RuleFor(x => x.MembershipId)
                .GreaterThan(0)
                .WithMessage("MembershipId is required");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid membership status");

            RuleFor(x => x.PaymentAmount)
                .GreaterThan(0)
                .WithMessage("Payment amount must be greater than zero")
                .When(x => x.PaymentAmount.HasValue);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
