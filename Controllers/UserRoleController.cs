using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/user-roles")]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var result = await _userRoleService.GetUserRolesAsync(userId);
            return ApiResponse.Success(result!, "User roles retrieved successfully");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleReqDto request)
        {
            var result = await _userRoleService.AssignRoleAsync(request.UserId, request.RoleId);
            return ApiResponse.Success(new { }, "Role assigned success");
        }

        [HttpPost("assign-multiple")]
        public async Task<IActionResult> AssignRoles([FromBody] AssignRolesReqDto request)
        {
            var result = await _userRoleService.AssignRolesAsync(request.UserId, request.RoleIds);
            return ApiResponse.Success(new { }, "Roles assigned success");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleReqDto request)
        {
            var result = await _userRoleService.RemoveRoleAsync(request.UserId, request.RoleId);
            return ApiResponse.Success(new { }, "Role removed success");
        }

        [HttpDelete("Remove-multiple")]
        public async Task<IActionResult> RemovesRole([FromBody] AssignRolesReqDto request)
        {
            var result = await _userRoleService.RemoveRolesAsync(request.UserId, request.RoleIds);
            return ApiResponse.Success(new { }, "Role removed success");
        }

    }
}
