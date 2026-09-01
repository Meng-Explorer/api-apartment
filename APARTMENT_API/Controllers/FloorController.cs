using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APARTMENT_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floor;
        public FloorController(IFloorService floor)
        {
            _floor = floor;
        }

        [HttpGet]
        public async Task<IActionResult> GetFloorAsynce(int page = 1, int pageSize = 10)
        {
            var data = await _floor.GetFloorAsynce(page, pageSize);
            return ApiResponse.Success(data);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFloorAsynce()
        {
            var data = await _floor.GetAllFloorAsynce();
            return ApiResponse.Success(data);
        }


        [HttpGet("{floorId}")]
        public async Task<IActionResult> GetFloorByIdAsynce(int floorId)
        {
            var data = await _floor.GetFloorByIdAsynce(floorId);
            return ApiResponse.Success(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post(FloorReqDto floorReqDto)
        {
            var data = await _floor.CreateAsynce(floorReqDto);
            return ApiResponse.Success(data);
        }
        [HttpPut("{floorId}")]
        public async Task<IActionResult> Put(int floorId, FloorReqDto floorReqDto)
        {
            var data = await _floor.UpdateAsynce(floorId, floorReqDto);
            return ApiResponse.Success(data);
        }
        [HttpDelete("{floorId}")]
        public async Task<IActionResult> Delete(int floorId)
        {
            await _floor.DeleteAsynce(floorId);
            return ApiResponse.Success(new { }, $"Delete building id {floorId} Successfully.");
        }
    }
}
