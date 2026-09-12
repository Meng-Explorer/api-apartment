namespace APARTMENT_API.DTOs.Request
{
    public class AssignRolesReqDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = [];
    }
}
