using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Tenant.Commands.UpdateTenant
{
    public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
    {
        public UpdateTenantCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.LogoUrl)
                .MaximumLength(500).WithMessage("Logo URL cannot exceed 500 characters");
        }
    }
}
