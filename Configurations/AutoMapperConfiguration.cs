using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Models;
using AutoMapper;

namespace APARTMENT_API.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<BuildingReqDto, Building>().ReverseMap();
            CreateMap<Building, BuildingResDto>().ReverseMap();

            CreateMap<FloorReqDto, Floor>().ReverseMap();
            CreateMap<Floor, FloorResDto>().ReverseMap();

            CreateMap<GuestReqDto,Guest>().ReverseMap();
            CreateMap<Guest,GuestResDto>().ReverseMap();

            CreateMap<RegisterReqDto, ApplicationUser>()
                .ForMember(destination => destination.PasswordHash, options => options.Ignore());
            CreateMap<ApplicationUser, UserResDto>().ReverseMap();

            CreateMap<ApplicationRole, RoleReqDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleResDto>().ReverseMap();

            CreateMap<ApplicationUserRole, UserRoleReqDto>().ReverseMap();
            CreateMap<ApplicationUserRole, UserRoleResDto>().ReverseMap();

            CreateMap<ApplicationPermission, PermissionReqDto>().ReverseMap();
            CreateMap<ApplicationPermission, PermissionResDto>().ReverseMap();

            CreateMap<ApplicationRolePermission,AssignRolePermissionReqDto>().ReverseMap();
            CreateMap<ApplicationRolePermission,RolePermissionsResDto>().ReverseMap();
           
        }
    }
}