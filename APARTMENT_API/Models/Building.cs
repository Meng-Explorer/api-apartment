using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TblBuilding")]
    public class Building
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(50)]
        [Column("NameEnglish",TypeName = "varchar2"),MaxLength(50)]
        public string? NameEnglish { get; set; }
        [Required, StringLength(50)]
        [Column("NameKhmer",TypeName ="nvarchar2"),MaxLength(50)]
        public string? NameKhmer { get; set; }
    }
}
