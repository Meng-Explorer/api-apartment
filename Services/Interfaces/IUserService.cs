using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Helpers;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<PageResult<UserResDto>> GetUsersByPageAsync(int page = 1, int pageSize = 10);
        Task<List<UserResDto>> GetUsersAsync();
        Task<UserResDto?> GetUserByIdAsync(int userId);
        Task<LoginResDto> Login(LoginReqDto request);
        Task<UserResDto> Register(RegisterReqDto request);
        Task<LoginResDto> ExternalLoginAsync(ExternalAuthDto request);
    }
}

