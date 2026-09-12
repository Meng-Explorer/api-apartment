using APARTMENT_API.Helpers;
using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PageResult<ApplicationUser>> GetUserByPageAsync(int page = 1, int pageSize = 10);
        Task<List<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(int userId);
        Task<ApplicationUser?> GetUserByNameAsync(string username);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> Login(ApplicationUser request);
        Task<ApplicationUser> Register(ApplicationUser request);
    }
}
