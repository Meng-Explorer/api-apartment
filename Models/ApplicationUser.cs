using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TBLAPPUSER")]
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class ApplicationUser
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required,MaxLength(50)]
        [Column("USERNAME")]
        public string Username { get; set; } = string.Empty;

        [Required,MaxLength(50)]
        [Column("EMAIL")]
        public string? Email { get; set; }

        [Required,MaxLength(100)]
        [Column("FULLNAME")]
        public string? FullName { get; set; }

        [Required, MaxLength(500)]
        [Column("PASSWORDHASH")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("ISACTIVE")]
        public int IsActive { get; set; } = 1;

        [Column("CREATEDAT")]
        public DateTime CreatedAt { get; set; } 

    }
}
