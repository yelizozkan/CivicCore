using AssociationMembership.Application.Common.Results;
using MediatR;

namespace AssociationMembership.Application.Features.Auth.Commands
{
    public class LogoutCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
    }
} 
