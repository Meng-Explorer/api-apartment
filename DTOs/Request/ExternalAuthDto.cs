namespace APARTMENT_API.DTOs.Request
{
    public class ExternalAuthDto
    {
        public string Provider { get; set; } = string.Empty;
        public string IdToken { get; set; } = string.Empty;
        // Add For Apple cause Token of Apple don't have name
        public string? FullName { get; set; }
    }
}
