using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Helpers;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IGuestService
    {
        Task<PageResult<GuestResDto>> GetGuestByPage(int page = 1, int pageSize = 10);
        Task<List<GuestResDto>> GetAllGuestAsync();
        Task<GuestResDto> GetGuestByIdAsync(int guestId);
        Task<GuestResDto> CreateGuestAsync(GuestReqDto reqDto);
        Task<GuestResDto> UpdateGuestAsync(int guestId,GuestReqDto reqDto);
        Task<bool> DeleteGuestAsync(int guestId);
    }
}
