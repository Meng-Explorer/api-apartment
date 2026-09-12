namespace APARTMENT_API.DTOs.Response
{
    public class UserRoleResDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public List<RoleResDto> Roles { get; set; } = [];
    }
}
