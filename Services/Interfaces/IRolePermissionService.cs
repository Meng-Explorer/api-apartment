using APARTMENT_API.DTOs.Response;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IRolePermissionService
    {
        Task<RolePermissionsResDto?> GetRolesPermissionsAsync(int roleId);
        Task<bool> AssignPermissionAsync(int roleId, int permissionId);
        Task<bool> AssignPermissionsAsync(int roleId, List<int> permissionIds);
        Task<bool> RemovePermissionAsync(int roleId, int permissionId);
        Task<bool> RemovePermissionsAsync(int roleId, List<int> permissionIds);
    }
}
