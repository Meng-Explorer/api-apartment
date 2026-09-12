using APARTMENT_API.Models;

namespace APARTMENT_API.Repositories.Interfaces
{
    public interface IRolePermissionRepository
    {
       
        Task<List<ApplicationRolePermission>> GetRolePermissionsAsync(int roleId);
        Task<ApplicationRolePermission?> GetRolePermissionAsync(int roleId, int permissionId);
        Task<List<ApplicationRolePermission>> GetRolePermissionEntitiesAsync(int roleId);
        Task<bool> HasPermissionAsync(int roleId, int permissionId);
        Task AddRolePermissionAsync(ApplicationRolePermission rolePermission);
        Task AddRolePermissionsAsync(IEnumerable<ApplicationRolePermission> rolePermissions);
        void RemoveRolePermission(ApplicationRolePermission rolePermission);
        void RemoveRolePermissions(IEnumerable<ApplicationRolePermission> rolePermissions);
        Task<int> SaveChangesAsync();

    }
}
