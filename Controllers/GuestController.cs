using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestController(IGuestService guestService) 
        { 
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestByPage(int page = 1 ,int pageSize = 10)
        {
            var data = await _guestService.GetGuestByPage(page, pageSize);
            return ApiResponse.Success(data);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllGuests()
        {
            var data = await _guestService.GetAllGuestAsync();
            return ApiResponse.Success(data);
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestById(int guestId)
        {
            var data = await _guestService.GetGuestByIdAsync(guestId);
            return ApiResponse.Success(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest([FromForm] GuestReqDto reqDto)
        {
            var data = await _guestService.CreateGuestAsync(reqDto);
            return ApiResponse.Success(data);
        }

        [HttpPut("{guestId}")]
        public async Task<IActionResult> UpdateGuest([FromRoute] int guestId, [FromForm] GuestReqDto reqDto)
        {
            var data = await _guestService.UpdateGuestAsync(guestId, reqDto);
            return ApiResponse.Success(data);
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteGuest(int guestId)
        {
            await _guestService.DeleteGuestAsync(guestId);
            return ApiResponse.Success(new { }, $"Delete guest ID : {guestId} Success");
        }




    }
}
