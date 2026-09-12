using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResDto>> GetAllAsync();
        Task<RoleResDto?> GetByIdAsync(int Id);
        Task<RoleResDto> CreateAsync(RoleReqDto request);
        Task<RoleResDto?> UpdateAsync(int Id, RoleReqDto request);
        Task<bool> DeleteAsync(int Id);
    }
}
