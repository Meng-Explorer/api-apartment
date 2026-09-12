using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _permissionService.GetAllAsync();
            return ApiResponse.Success(data);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _permissionService.GetByIdAsync(id);
            return ApiResponse.Success(data!);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionReqDto request)
        {
            var data = await _permissionService.CreateAsync(request);
            return ApiResponse.Success(data);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionReqDto request)
        {
            var data = await _permissionService.UpdateAsync(id, request);
            return ApiResponse.Success("Role Updated Success");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _permissionService.DeleteAsync(id);
            return ApiResponse.Success(new { });
        }
    }
}


