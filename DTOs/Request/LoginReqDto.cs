namespace APARTMENT_API.DTOs.Request
{
    public class LoginReqDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
    }
}
