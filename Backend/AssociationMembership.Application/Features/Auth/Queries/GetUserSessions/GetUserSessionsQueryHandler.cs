using AssociationMembership.Application.Features.Auth.Queries;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Application.DTOs.Auth;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Features.Auth.Handlers
{
    public class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, Result<IEnumerable<SessionDto>>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetUserSessionsQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Result<IEnumerable<SessionDto>>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tokens = await _refreshTokenRepository.GetUserTokensAsync(request.UserId, includeRevoked: true);
                
                var sessions = tokens.Select(token => new SessionDto
                {
                    Token = token.Token,
                    CreatedDate = token.CreatedDate,
                    ExpiryDate = token.ExpiryDate,
                    IsActive = token.IsActive,
                    IpAddress = token.IpAddress ?? "Unknown",
                    UserAgent = token.UserAgent ?? "Unknown",
                    DeviceId = token.DeviceId ?? "Unknown",
                    DeviceName = token.DeviceName ?? "Unknown",
                    DeviceType = token.DeviceType ?? "Unknown",
                    Location = token.Location ?? "Unknown",
                    RemainingTime = token.GetRemainingTime(),
                    IsCurrentSession = false // TODO: Implement current session detection
                });

            return Result<IEnumerable<SessionDto>>.Success(sessions);
        }
            catch (Exception ex)
            {
            return Result<IEnumerable<SessionDto>>.Failure(Error.Failure(
               ErrorCode.InvalidOperation,
               $"Error retrieving user sessions: {ex.Message}"));
            }
        }
    }
} 
