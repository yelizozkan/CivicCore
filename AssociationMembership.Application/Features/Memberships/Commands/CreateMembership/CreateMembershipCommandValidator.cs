using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Memberships.Commands.CreateMembership
{
    public class CreateMembershipCommandValidator : AbstractValidator<CreateMembershipCommand>
    {
        public CreateMembershipCommandValidator()
        {
            RuleFor(x => x.TenantGroupId)
                .GreaterThan(0)
                .WithMessage("TenantGroupId is required");

            RuleFor(x => x.Membership)
                .NotNull()
                .WithMessage("Membership data is required");

            When(x => x.Membership != null, () =>
            {

                RuleFor(x => x.Membership.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

                RuleFor(x => x.Membership.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

                RuleFor(x => x.Membership.IdentityNumber)
                    .NotEmpty().WithMessage("Identity number is required")
                    .Length(11).WithMessage("Identity number must be exactly 11 characters")
                    .Matches(@"^\d{11}$").WithMessage("Identity number must contain only digits");

                RuleFor(x => x.Membership.BirthDate)
                    .NotEmpty().WithMessage("Birth date is required")
                    .LessThan(DateTime.Now.AddYears(-18))
                    .WithMessage("Member must be at least 18 years old");


                RuleFor(x => x.Membership.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

                RuleFor(x => x.Membership.MobilePhone)
                    .NotEmpty().WithMessage("Mobile phone is required")
                    .MaximumLength(20).WithMessage("Mobile phone cannot exceed 20 characters");

                RuleFor(x => x.Membership.ResidenceAddress)
                    .NotEmpty().WithMessage("Residence address is required")
                    .MaximumLength(1000).WithMessage("Address cannot exceed 1000 characters");

                RuleFor(x => x.Membership.WorkPhone)
                    .Matches(@"^[0-9]{10,15}$")
                    .WithMessage("Work phone must be 10-15 digits")
                    .When(x => !string.IsNullOrEmpty(x.Membership.WorkPhone));

                RuleFor(x => x.Membership.MotivationText)
                    .MaximumLength(2000)
                    .WithMessage("Motivation text cannot exceed 2000 characters")
                    .When(x => !string.IsNullOrEmpty(x.Membership.MotivationText));


                RuleFor(x => x.Membership.KvkkAccepted)
                    .Equal(true)
                    .WithMessage("KVKK consent must be accepted");

                RuleFor(x => x.Membership.ExplicitConsentAccepted)
                    .Equal(true)
                    .WithMessage("Explicit consent must be accepted");



            });
        }
    }

}
