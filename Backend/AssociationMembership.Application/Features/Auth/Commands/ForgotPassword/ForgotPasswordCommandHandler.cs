using AssociationMembership.Application.Features.Auth.Commands;
using AssociationMembership.Application.Services;
using AssociationMembership.Application.Common.Results;
using MediatR;


namespace AssociationMembership.Application.Features.Auth.Handlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement forgot password logic
            return Result.Success();
        }
    }
} 
