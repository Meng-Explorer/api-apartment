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
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _building;
        public BuildingController(IBuildingService building)
        {
            _building = building;
        }
        [HttpGet]
        public async Task<IActionResult> GetBuilding(int page = 1, int pageSize = 10)
        {
            var data = await _building.GetBuilding(page, pageSize);
            return ApiResponse.Success(data);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBuilding()
        {
            var data = await _building.GetAllBuild();
            return ApiResponse.Success(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuilding(int id)
        {
            var data = await _building.GetBuilding(id);
            return ApiResponse.Success(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(BuildingReqDto buildingDto)
        {
            var data = await _building.Create(buildingDto);
            return ApiResponse.Success(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, BuildingReqDto buildingDto)
        {
            var data = await _building.Update(id, buildingDto);
            return ApiResponse.Success(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _building.Delete(id);
            return ApiResponse.Success(new {} ,$"Delete building id {id} Successfully.");
        }


     }
}
