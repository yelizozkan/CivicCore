using AssociationMembership.Application.Features.Auth.Commands;
using AssociationMembership.Application.Services;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;

namespace AssociationMembership.Application.Features.Auth.Handlers
{
    public class LogoutDeviceCommandHandler : IRequestHandler<LogoutDeviceCommand, Result>
    {
        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;

        public LogoutDeviceCommandHandler(IAuthService authService, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(LogoutDeviceCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
            return Result<int>.Failure(Error.Failure(
                   ErrorCode.NotFound,
                   "User not authenticated"));

        return await _authService.RevokeTokensByDeviceAsync(userId.Value, request.DeviceId, request.IpAddress, request.UserAgent, request.Reason);
        }
    }
} 
