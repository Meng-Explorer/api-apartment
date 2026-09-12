using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<ApplicationRole>> GetAllAsync();
        Task<ApplicationRole?> GetByIdAsync(int id);
        Task<ApplicationRole?> GetByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name, int excludeId);
        Task AddAsync(ApplicationRole role);
        Task Update(ApplicationRole role);
        void Delete(ApplicationRole role);
        Task<bool> HasUsersAsync(int roleId);
        Task<int> SaveChangesAsync();
    }
}
