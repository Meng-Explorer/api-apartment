using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APARTMENT_API.Models
{
    [Table("TBLAPPPERMISSION")]
    public class ApplicationPermission
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(230)]
        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;
    }
}

