namespace APARTMENT_API.DTOs.Request
{
    public class RemoveRolePermissionsReqDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = [];
    }
}
