using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionResDto>> GetAllAsync();
        Task<PermissionResDto?> GetByIdAsync(int Id);
        Task<PermissionResDto> CreateAsync(PermissionReqDto request);
        Task<PermissionResDto?> UpdateAsync(int Id, PermissionReqDto request);
        Task<bool> DeleteAsync(int Id);
    }
}
