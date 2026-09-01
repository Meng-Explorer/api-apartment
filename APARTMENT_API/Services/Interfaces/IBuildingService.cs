using APARTMENT_API.Controllers;
using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IBuildingService
    {
        Task<PageResult<BuildingResDto>> GetBuilding(int page =1, int pageSize = 10);
        Task<List<BuildingResDto>> GetAllBuild();
        Task<BuildingResDto> GetBuilding(int id);
        Task<BuildingResDto> Create(BuildingReqDto building);
        Task<BuildingResDto> Update(int id,BuildingReqDto building);
        Task<bool> Delete(int id);
    }
}
