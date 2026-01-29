using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.TenantGroup.Commands.CreateTenantGroup
{
    public class CreateTenantGroupCommandValidator : AbstractValidator<CreateTenantGroupCommand>
    {
        public CreateTenantGroupCommandValidator()
        {
            RuleFor(x => x.TenantId)
                .GreaterThan(0).WithMessage("TenantId is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(150).WithMessage("Name cannot exceed 150 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");
        }
    }
}
