namespace APARTMENT_API.DTOs.Response
{
    public class RolePermissionsResDto
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<PermissionResDto> Permissions { get; set; } = [];
    }
}
