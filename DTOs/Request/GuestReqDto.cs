namespace APARTMENT_API.DTOs.Request
{
    public class GuestReqDto
    {
        public string? NameEnglish { get; set; }
        public string? NameKhmer { get; set; }
        public string? Sex { get; set; }
        public DateTime? Dob { get; set;  }
        public string? Address { get; set; }
        public string? Nationality { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Ssn { get; set; }
        public string? Passport { get; set; }
        public string? Status { get; set; }
        public IFormFile? Image { get; set; }
    }
}
