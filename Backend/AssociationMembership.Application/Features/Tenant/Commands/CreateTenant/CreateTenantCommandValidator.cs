using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Commands.CreateTenant
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required")
                .MaximumLength(50).WithMessage("Code cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("Code can only contain letters, numbers, underscores and hyphens");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.LogoUrl)
                .MaximumLength(500).WithMessage("Logo URL cannot exceed 500 characters");
        }
    }
}
