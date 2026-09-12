using APARTMENT_API.Configurations;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly ApplicationDbContext _context;
        public RolePermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationRolePermission>> GetRolePermissionsAsync(int roleId)
        {
            return await _context.TblAppRolePermission
                .AsNoTracking()
                .Where(x => x.RoleId == roleId)
                .Include(x => x.PermissionId) // if u wanna show data Permission detail
                .ToListAsync();
        }

        public async Task<ApplicationRolePermission?> GetRolePermissionAsync(int roleId,int permissionId)
        {
            return await _context.TblAppRolePermission
                .FirstOrDefaultAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);
        }
        public async Task<List<ApplicationRolePermission>> GetRolePermissionEntitiesAsync(int roleId)
        {
            return await _context.TblAppRolePermission
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<bool> HasPermissionAsync(int roleId,int permissionId)
        {
            return await _context.TblAppRolePermission
                //.CountAsync(x => x.UserId == userId && x.RoleId == roleId) > 0;
                .AnyAsync(x => x.RoleId == roleId && x.PermissionId == permissionId);
        }

        public async Task AddRolePermissionAsync(ApplicationRolePermission rolePermission)
        {
            await _context.TblAppRolePermission.AddAsync(rolePermission);
        }
        public async Task AddRolePermissionsAsync(IEnumerable<ApplicationRolePermission> rolePermissions)
        {
            await _context.TblAppRolePermission.AddRangeAsync(rolePermissions);
        }

        public void RemoveRolePermission(ApplicationRolePermission rolePermission)
        {
            _context.TblAppRolePermission.Remove(rolePermission);
        }

        public void RemoveRolePermissions(IEnumerable<ApplicationRolePermission> rolePermissions)
        {
            _context.TblAppRolePermission.RemoveRange(rolePermissions);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
