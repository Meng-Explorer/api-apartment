using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Helpers;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APARTMENT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize] 
        [HttpGet("page")]
        public async Task<IActionResult> GetUsersByPage([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var data = await _userService.GetUsersByPageAsync(page, pageSize);
            return ApiResponse.Success(data, "Users retrieved successfully");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetUsersAsync();
            return ApiResponse.Success(data, "All users retrieved successfully");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var data = await _userService.GetUserByIdAsync(id);

            if (data == null)
            {
                return NotFound(ApiResponse.Error("User not found"));
            }

            return ApiResponse.Success(data, "User retrieved successfully");
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterReqDto request)
        {
            var data = await _userService.Register(request);
            return ApiResponse.Success(data, "Register Success");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto request)
        {
            var data = await _userService.Login(request);
            return ApiResponse.Success(data, "Login Success");
        }

        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto request)
        {
            if (string.IsNullOrEmpty(request.Provider) || string.IsNullOrEmpty(request.IdToken))
            {
                return BadRequest(ApiResponse.Error("Provider and IdToken are required"));
            }

            var data = await _userService.ExternalLoginAsync(request);
            return ApiResponse.Success(data, "Login Success");
        }
        
        [Authorize]
        [HttpGet("getProfile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var fullName = User.FindFirstValue("FullName");
            var roles = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
            return ApiResponse.Success(new
            {
                userId,
                username,
                email,
                fullName,
                roles
            });

        }

    }
}
