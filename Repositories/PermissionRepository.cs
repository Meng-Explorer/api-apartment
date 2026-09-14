using APARTMENT_API.Configurations;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;
        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationPermission>> GetAllAsync()
        {
            return await _context.TblAppPermission
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ApplicationPermission?> GetByIdAsync(int id)
        {
            return await _context.TblAppPermission
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationPermission?> GetByNameAsync(string name)
        {
            return await _context.TblAppPermission
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.TblAppPermission
                .CountAsync(x => x.Name == name) > 0;
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludeId)
        {
            return await _context.TblAppPermission
                .CountAsync(x => x.Name == name && x.Id != excludeId) > 0;
        }

        public async Task AddAsync(ApplicationPermission permission)
        {
            await _context.TblAppPermission.AddAsync(permission);
        }
        public void Update(ApplicationPermission permission)
        {
            _context.TblAppPermission.Update(permission);
        }

        public void Delete(ApplicationPermission permission)
        {
            _context.TblAppPermission.Remove(permission);
        }

        public async Task<bool> HasRolesAsync(int permissionId)
        {
            return await _context.TblAppRolePermission
                .CountAsync(x => x.PermissionId == permissionId) > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}