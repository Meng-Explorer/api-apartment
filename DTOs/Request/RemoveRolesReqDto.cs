namespace APARTMENT_API.DTOs.Request
{
    public class RemoveRolesReqDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = [];
    }
}
