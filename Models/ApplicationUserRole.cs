using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TBLAPPUSERROLE")]
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class ApplicationUserRole
    {
        [Column("USERID")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;

        [Column("ROLEID")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; } = null!;
    }
}
