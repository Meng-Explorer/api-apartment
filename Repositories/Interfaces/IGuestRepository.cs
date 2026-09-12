using APARTMENT_API.Helpers;
using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IGuestRepository
    {
        Task<PageResult<Guest>> GetGuestByPage(int page = 1, int pageSize = 10);
        Task<List<Guest>> GatAllGuestAsync();
        Task<Guest> GetGuestByIdAsync(int guestId);
        Task<Guest> CreateGuestAsync(Guest guest);
        Task<Guest> UpdateGuestAsync(Guest guest);
        Task<bool> DeleteGuestAsync(Guest guest);
    }
}
