using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizationService _service;
        public AuthController(IAuthorizationService service) 
        {
         _service = service;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthReqDto authReq)
        {
            var data = await _service.Login(authReq);
            if (data == null)
            {
                return new ObjectResult(new { Success = false, StatusCode = 401, Message = "Invalid username or password", Data = new { } }) { StatusCode = 401 };
            }
            return ApiResponse.Success(data);
        }
    }
}
