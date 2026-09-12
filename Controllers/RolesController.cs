using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _roleService.GetAllAsync();
            return ApiResponse.Success(data);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _roleService.GetByIdAsync(id);
            return ApiResponse.Success(data!);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleReqDto request)
        {
            var data = await _roleService.CreateAsync(request);
            return ApiResponse.Success(data);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleReqDto request)
        {
            var data = await _roleService.UpdateAsync(id, request);
            return ApiResponse.Success("Role Updated Success");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteAsync(id);
            return ApiResponse.Success(new { });
        }

    }
}
