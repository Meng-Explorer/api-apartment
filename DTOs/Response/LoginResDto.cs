using APARTMENT_API.Models;

namespace APARTMENT_API.DTOs.Response
{
    public class LoginResDto
    {
        public UserResDto? User { get; set;}
        public string? Token { get; set;}
        public List<ApplicationRole> Roles { get; set; } = [];
    }
}
