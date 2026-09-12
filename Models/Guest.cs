using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TblGuest")]
    public class Guest
    {
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("NameEnglish", TypeName = "varchar2")]
        [StringLength(50)]
        public string? NameEnglish { get; set; }

        [Column("NameKhmer", TypeName = "nvarchar2")]
        [StringLength(50)]
        public string? NameKhmer { get; set; }

        [Column("Sex", TypeName = "varchar2")]
        [StringLength(10)]
        public string? Sex { get; set; }

        [Column("DOB", TypeName = "DATE")]
        public DateTime? Dob { get; set; }

        [Column("Address", TypeName = "nvarchar2")]
        [StringLength(100)]
        public string? Address { get; set; }

        [Column("Nationality", TypeName = "varchar2")]
        [StringLength(50)]
        public string? Nationality { get; set; }

        [Column("Phone", TypeName = "varchar2")]
        [StringLength(50)]
        public string? Phone { get; set; }

        [Column("Email", TypeName = "varchar2")]
        [StringLength(20)]
        public string? Email { get; set; }

        [Column("SSN", TypeName = "varchar2")]
        [StringLength(20)]
        public string? Ssn { get; set; }

        [Column("Passport", TypeName = "varchar2")]
        [StringLength(20)]
        public string? Passport { get; set; }

        [Column("Status", TypeName = "varchar2")]
        [StringLength(20)]
        public string? Status { get; set; }

        [Column("Image", TypeName = "Varchar2")]
        [StringLength(200)]
        public string? ImagePath { get; set; }
    }
}