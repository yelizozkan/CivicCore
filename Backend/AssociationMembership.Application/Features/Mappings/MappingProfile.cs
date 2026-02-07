using AssociationMembership.Application.DTOs;
using AssociationMembership.Application.DTOs.Auth;
using AssociationMembership.Application.Features.Auth.Commands;
using AssociationMembership.Application.Features.Memberships.Dtos;
using AssociationMembership.Application.Features.Roles.Commands;
using AssociationMembership.Application.Features.Tenant.Dtos;
using AssociationMembership.Application.Features.TenantGroup.Dtos;
using AssociationMembership.Application.Features.Users.Commands;
using AssociationMembership.Domain.Common.Enums;
using AssociationMembership.Domain.Entities;
using AutoMapper;

namespace AssociationMembership.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => 
                    src.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission)).Distinct()));

            // User list mapping (without permissions for performance)
            CreateMap<User, UserListDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)); // This will be hashed in handler

            CreateMap<RegisterRequestDto, RegisterCommand>();
            CreateMap<LoginRequestDto, LoginCommand>();
            CreateMap<RefreshTokenRequestDto, RefreshTokenCommand>();

            // Role mappings
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => 
                    src.RolePermissions.Select(rp => rp.Permission).Where(p => p != null).ToList()));

            CreateMap<CreateRoleCommand, Role>()
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            CreateMap<UpdateRoleCommand, Role>()
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            // Permission mappings
            CreateMap<Permission, PermissionDto>()
                .ForMember(dest => dest.IndividualPermissions, opt => opt.MapFrom(src => 
                    src.Type.GetIndividualPermissions().Select(p => p.ToString()).ToList()));

           

            // UserRole mappings
            CreateMap<UserRole, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status))
                .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.User.LastLoginDate))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.User.EmailConfirmed))
                .ForMember(dest => dest.PhoneConfirmed, opt => opt.MapFrom(src => src.User.PhoneConfirmed))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.User.CreatedDate))
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());

            // RolePermission mappings
            CreateMap<RolePermission, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Role.Description))
                .ForMember(dest => dest.IsSystemRole, opt => opt.MapFrom(src => src.Role.IsSystemRole))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Role.CreatedDate))
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());

            // RefreshToken mappings
            CreateMap<RefreshToken, LoginResponseDto>()
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiryDate))
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());



            // CreateMembershipDto ? Membership Entity mappings
            CreateMap<CreateMembershipDto, Membership>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TenantGroupId, opt => opt.Ignore())
                .ForMember(dest => dest.ConsentAcceptedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TenantGroup, opt => opt.Ignore())
                .ForMember(dest => dest.Tracking, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // UpdateMembershipDto ? Membership Entity (for updates) mappings
            CreateMap<UpdateMembershipDto, Membership>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TenantGroupId, opt => opt.Ignore())
                .ForMember(dest => dest.IdentityNumber, opt => opt.Ignore()) // Cannot change
                .ForMember(dest => dest.KvkkAccepted, opt => opt.Ignore()) // Cannot change
                .ForMember(dest => dest.ExplicitConsentAccepted, opt => opt.Ignore()) // Cannot change
                .ForMember(dest => dest.ConsentAcceptedDate, opt => opt.Ignore())
                .ForMember(dest => dest.TenantGroup, opt => opt.Ignore())
                .ForMember(dest => dest.Tracking, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            // Membership Entity ? MembershipDto (full details) mappings
            CreateMap<Membership, MembershipDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.Status : MembershipStatus.Pending))
                .ForMember(dest => dest.PaymentAmount,
                    opt => opt.MapFrom(src => src.Tracking != null ? (decimal?)src.Tracking.PaymentAmount : null))
                .ForMember(dest => dest.PaymentReceivedDate,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.PaymentReceivedDate : null))
                .ForMember(dest => dest.ApprovedDate,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.ApprovedDate : null))
                .ForMember(dest => dest.TrackingDescription,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.Description : null));

            // Membership Entity ? MembershipListDto (summary) mappings
            CreateMap<Membership, MembershipListDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.Status : MembershipStatus.Pending))
                .ForMember(dest => dest.ApprovedDate,
                    opt => opt.MapFrom(src => src.Tracking != null ? src.Tracking.ApprovedDate : null));


            // Tenant Mappings
            CreateMap<Tenant, TenantDto>();
            CreateMap<Tenant, TenantWithGroupsDto>();
            CreateMap<CreateTenantDto, Tenant>();
            CreateMap<UpdateTenantDto, Tenant>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.TenantGroups, opt => opt.Ignore());

            // TenantGroup Mappings
            CreateMap<TenantGroup, TenantGroupDto>();
            CreateMap<CreateTenantGroupDto, TenantGroup>();
            CreateMap<UpdateTenantGroupDto, TenantGroup>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TenantId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Tenant, opt => opt.Ignore())
                .ForMember(dest => dest.Memberships, opt => opt.Ignore());

        }
    }
} 
