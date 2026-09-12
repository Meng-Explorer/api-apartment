using APARTMENT_API.Models;
using APARTMENT_API.Helpers;


namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IBuildingRepository
    {
        Task<PageResult<Building>> GetBuilding(int page = 1, int pageSize = 10);
        Task<List<Building>> GetAllBuild();
        Task<Building> GetBuilding(int id);
        Task<Building> Create(Building building);
        Task<Building> Update(Building building);
        Task<bool> Delete(Building building);
    }
}
