using AssociationMembership.Application.DTOs.Auth;
using AssociationMembership.Application.Common.Results;
using MediatR;

namespace AssociationMembership.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<Result<LoginResponseDto>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
} 
