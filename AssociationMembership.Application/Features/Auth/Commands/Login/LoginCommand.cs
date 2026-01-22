using AssociationMembership.Application.DTOs.Auth;
using AssociationMembership.Application.Common.Results;
using MediatR;

namespace AssociationMembership.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<Result<LoginResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
} 
