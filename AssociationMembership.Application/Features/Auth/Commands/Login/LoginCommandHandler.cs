using AutoMapper;
using AssociationMembership.Application.Features.Auth.Commands;
using AssociationMembership.Application.Services;
using AssociationMembership.Application.Common.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<Application.DTOs.Auth.LoginResponseDto>>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<Application.DTOs.Auth.LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Email, request.Password, request.IpAddress, request.UserAgent, request.DeviceId, request.DeviceName);
        }
    }
} 
