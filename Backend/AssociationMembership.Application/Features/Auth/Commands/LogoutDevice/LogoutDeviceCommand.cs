using AssociationMembership.Application.Common.Results;
using MediatR;

namespace AssociationMembership.Application.Features.Auth.Commands
{
    public class LogoutDeviceCommand : IRequest<Result>
    {
        public string DeviceId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string Reason { get; set; }
    }
} 
