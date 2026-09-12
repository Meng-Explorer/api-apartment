using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Helpers;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IFloorService
    {
        Task<PageResult<FloorResDto>> GetFloorAsynce(int page = 1, int pageSize = 10);
        Task<List<FloorResDto>> GetAllFloorAsynce();
        Task<FloorResDto> GetFloorByIdAsynce(int floorId);
        Task<FloorResDto> CreateAsynce(FloorReqDto req);
        Task<FloorResDto> UpdateAsynce(int floorId, FloorReqDto req);
        Task<bool> DeleteAsynce(int floorId);
    }
}
