using APARTMENT_API.DTOs.Response;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleResDto?> GetUserRolesAsync(int userId);
        Task<bool> AssignRoleAsync(int userId, int roleId);
        Task<bool> AssignRolesAsync(int userId, List<int> roleIds);
        Task<bool> RemoveRoleAsync(int userId, int roleId);
        Task<bool> RemoveRolesAsync(int userId, List<int> roleIds);
    }
}

