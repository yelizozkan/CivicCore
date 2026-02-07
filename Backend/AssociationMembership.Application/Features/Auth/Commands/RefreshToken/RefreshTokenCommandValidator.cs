using FluentValidation;
using AssociationMembership.Application.Features.Auth.Commands;

namespace AssociationMembership.Application.Validators.Auth
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Access token is required.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
} 
