namespace APARTMENT_API.DTOs.Request
{
    public class RoleReqDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int IsActive { get; set; }
    }
}
