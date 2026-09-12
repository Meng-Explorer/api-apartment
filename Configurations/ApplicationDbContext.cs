using APARTMENT_API.Models;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Configurations
{
    // 1. Fixed "DbContext" capitalization
    public class ApplicationDbContext : DbContext
    {
        // 2. Removed the accidental "public class" nested declaration.
        // This is the correct modern C# Primary Constructor syntax:
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
        public DbSet<Building> TblBuilding { get; set; }
        public DbSet<Floor> TblFloor { get; set; }
        public DbSet<Guest> TblGuest { get; set; }

        // Production Ready Role Security
        public DbSet<ApplicationUser> TblAppUser { get; set; }
        public DbSet<ApplicationRole> TblAppRole { get; set; }
        public DbSet<ApplicationUserRole> TblAppUserRole { get; set; }
        public DbSet<ApplicationPermission> TblAppPermission { get; set; }
        public DbSet<ApplicationRolePermission> TblAppRolePermission { get; set; }

        
    }
}


