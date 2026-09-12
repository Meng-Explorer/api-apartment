using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TBLAPPROLE")]
    public class ApplicationRole
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(230)]
        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        [Column("ISACTIVE")]
        public int IsActive { get; set; } = 1;
        
    }
}
