using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace APARTMENT_API.Models
{
    [Table("TBLAPPROLEPERMISSION")]
    [PrimaryKey(nameof(RoleId), nameof(PermissionId))]
    public class ApplicationRolePermission
    {
        [Column("ROLEID")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; } = null!;
        
        [Column("PERMISSIONID")]
        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public ApplicationRolePermission RolePermission { get; set; } = null!;
    }
}
