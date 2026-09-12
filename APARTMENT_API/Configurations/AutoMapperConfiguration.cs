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

            CreateMap<FloorReqDto, Floor>();
            CreateMap<Floor, FloorResDto>();
            
        }
    }
}