using APARTMENT_API.Configurations;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationRole>> GetUserRolesAsync(int userId)
        {
            return await _context.TblAppUserRole
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Role)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ApplicationUserRole?> GetUserRoleAsync(int userId,int roleId)
        {
            return await _context.TblAppUserRole
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
        public async Task<List<ApplicationUserRole>> GetUserRoleEntitiesASync(int userId)
        {
            return await _context.TblAppUserRole
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<bool> HashRoleAsync(int userId,int roleId)
        {
            return await _context.TblAppUserRole
                .CountAsync(x => x.UserId == userId && x.RoleId == roleId) > 0;
        }

        public async Task AddUserRoleAsync(ApplicationUserRole userRole)
        {
            await _context.TblAppUserRole.AddAsync(userRole);
        } 
        public async Task AddUserRolesAsync(IEnumerable<ApplicationUserRole> userRoles)
        {
            await _context.TblAppUserRole.AddRangeAsync(userRoles);
        }

        public void RemoveUserRole(ApplicationUserRole userRole)
        {
            _context.TblAppUserRole.Remove(userRole);
        }

        public void RemoveUserRoles(IEnumerable<ApplicationUserRole> userRoles)
        {
            _context.TblAppUserRole.RemoveRange(userRoles);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
