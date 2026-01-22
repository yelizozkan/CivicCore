using AssociationMembership.Application.Features.Auth.Commands;
using AssociationMembership.Application.Services;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Auth.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IPasswordService _passwordService;
    private readonly ICurrentUserService _currentUserService;

    public ChangePasswordCommandHandler(
        IPasswordService passwordService,
        ICurrentUserService currentUserService)
    {
        _passwordService = passwordService;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (!userId.HasValue)
            return Result<int>.Failure(Error.Failure(
               ErrorCode.NotFound,
               "User not authenticated"));

        // TODO: Implement password change logic
        return Result.Success();
    }
}
} 
