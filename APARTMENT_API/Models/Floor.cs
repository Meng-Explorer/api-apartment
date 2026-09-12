using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace APARTMENT_API.Models
{
    [Table("TblFloor")]
    public class Floor
    {
        [Key]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("FloorNo", TypeName = "number(3)")]
        public int FloorNo { get; set; }
        [Required]
        [Column("BuildingId", TypeName = "number")]
        public int BuildingId { get; set; }
        public Building? building { get; set; }
    }
}
