using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/role-permissions")]
    [Authorize]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;
        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRolePermissions(int roleId)
        {
            var result = await _rolePermissionService.GetRolesPermissionsAsync(roleId);
            return ApiResponse.Success(result!, "Role permissions retrieved successfully");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermission([FromBody] AssignRolePermissionReqDto request)
        {
            var result = await _rolePermissionService.AssignPermissionAsync(request.RoleId, request.PermissionId);
            return ApiResponse.Success(new { }, "Permission assigned success");
        }

        [HttpPost("assign-multiple")]
        public async Task<IActionResult> AssignPermissions([FromBody] AssignRolePermissionsReqDto request)
        {
            var result = await _rolePermissionService.AssignPermissionsAsync(request.RoleId, request.PermissionIds);
            return ApiResponse.Success(new { }, "Permissions assigned successfully");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemovePermission([FromBody] RemoveRolePermissionReqDto request)
        {
            var result = await _rolePermissionService.RemovePermissionAsync(request.RoleId, request.PermissionId);
            return ApiResponse.Success(new { }, "Permission removed successfully");
        }

        [HttpDelete("Remove-multiple")]
        public async Task<IActionResult> RemovePermissions([FromBody] RemoveRolePermissionsReqDto request)
        {
            var result = await _rolePermissionService.RemovePermissionsAsync(request.RoleId, request.PermissionIds);
            return ApiResponse.Success(new { }, "Permissions removed successfully");
        }
    }
}

