using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [ Table("TblUser")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,MaxLength(50),Column("Username",TypeName ="varchar2(50)")]
        public string? Username { get; set; }

        [Required,MaxLength(50),Column("Email",TypeName ="varchar2(50)")]
        public string? Email { get; set; }

        [Required, MaxLength(50), Column("Password", TypeName = "varchar2(50)")]
        public string? Password { get; set; }

        [Required, Column("Active", TypeName = "number(1)")]
        public bool Active { get; set; }    
    }
}
