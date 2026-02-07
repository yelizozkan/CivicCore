using AssociationMembership.Application.Common.Results;
using MediatR;

namespace AssociationMembership.Application.Features.Auth.Commands
{
    public class LogoutAllCommand : IRequest<Result>
    {
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
    }
} 
