using AutoMapper;
using AssociationMembership.Application.Features.Roles.Queries;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AssociationMembership.Application.DTOs;
using AssociationMembership.Domain.Common.Enums;

namespace AssociationMembership.Application.Features.Roles.Handlers
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetRoleWithPermissionsAsync(request.Id);
            
            if (role == null)
            return Result<RoleDto>.Failure(Error.Failure(
                       ErrorCode.NotFound,
                       "Role not found"));

        var roleDto = _mapper.Map<RoleDto>(role);
            return Result<RoleDto>.Success(roleDto);
    }
    }
} 
