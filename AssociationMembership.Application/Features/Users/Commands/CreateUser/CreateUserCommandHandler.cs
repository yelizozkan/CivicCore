using AutoMapper;
using AssociationMembership.Application.Features.Users.Commands;
using AssociationMembership.Application.Interfaces;
using AssociationMembership.Application.Services;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Domain.Entities;
using AssociationMembership.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AssociationMembership.Application.Common.Results;
using AssociationMembership.Application.DTOs;
using AssociationMembership.Domain.Common.Enums;

namespace AssociationMembership.Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<Result<UserListDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            if (await _unitOfWork.Users.EmailExistsAsync(request.Email))
            return Result<UserListDto>.Failure(Error.Failure(
                    ErrorCode.AlreadyExists,
                    "Email already exists"));

        // Hash password
        var passwordResult = _passwordService.HashPassword(request.Password);
            if (!passwordResult.IsSuccess)
            return Result<UserListDto>.Failure(Error.Failure(
                       ErrorCode.AlreadyExists,
                       $"{passwordResult.Error}"));

            // Create user
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordResult.Value,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                EmailConfirmed = false
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Assign roles if provided
            if (request.RoleIds?.Any() == true)
            {
                foreach (var roleId in request.RoleIds)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    };
                    await _unitOfWork.Users.AddUserRoleAsync(userRole);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            // Reload user with roles to get complete data for mapping
            var userWithRoles = await _unitOfWork.Users.GetUserWithRolesAsync(user.Id);
            var userDto = _mapper.Map<UserListDto>(userWithRoles);
            return Result<UserListDto>.Success(userDto);
        }
    }
} 
