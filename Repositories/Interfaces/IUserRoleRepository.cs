using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<ApplicationRole>> GetUserRolesAsync(int userId);
        Task<ApplicationUserRole?> GetUserRoleAsync(int userId,int roleId);
        Task<List<ApplicationUserRole>> GetUserRoleEntitiesASync(int userId);
        Task<bool> HashRoleAsync(int userId,int roleId);
        Task AddUserRoleAsync(ApplicationUserRole userRole);
        Task AddUserRolesAsync(IEnumerable<ApplicationUserRole> userRoles);
        void RemoveUserRole(ApplicationUserRole userRole);
        void RemoveUserRoles(IEnumerable<ApplicationUserRole> userRoles);
        Task<int> SaveChangesAsync();

    }
}
