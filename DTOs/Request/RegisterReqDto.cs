namespace APARTMENT_API.DTOs.Request
{
    public class RegisterReqDto
    {
        public string Username { get; set; } = string.Empty;
        public string? Email {  get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
