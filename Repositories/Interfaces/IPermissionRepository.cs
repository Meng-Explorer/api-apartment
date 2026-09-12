using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<ApplicationPermission>> GetAllAsync();
        Task<ApplicationPermission?> GetByIdAsync(int id);
        Task<ApplicationPermission?> GetByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name, int excludeId);
        Task AddAsync(ApplicationPermission permission);
        void Update(ApplicationPermission permission);
        void Delete(ApplicationPermission permission);
        Task<bool> HasRolesAsync(int permissionId);
        Task<int> SaveChangesAsync();
    }
}
