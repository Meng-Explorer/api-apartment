using APARTMENT_API.Helpers;
using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IFloorRepository
    {
        Task<PageResult<Floor>> GetFloorAsynce(int page = 1, int pageSize = 10);
        Task<List<Floor>> GetAllFloorAsynce();
        Task<Floor> GetFloorByIdAsynce(int floorId);
        Task<Floor>CreateAsynce(Floor reg);
        Task<Floor> UpdateAsynce(Floor req);
        Task<bool> DeleteAsynce(Floor req);
    }
}

