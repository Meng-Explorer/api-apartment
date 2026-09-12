using APARTMENT_API.Configurations;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task<List<ApplicationRole>> GetAllAsync()
        {
            return await _context.TblAppRole
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ApplicationRole?> GetByIdAsync(int id)
        {
            return await _context.TblAppRole
                .FirstOrDefaultAsync(x=> x.Id == id);
        } 

        public async Task<ApplicationRole?> GetByNameAsync(string name)
        {
            return await _context.TblAppRole
                .FirstOrDefaultAsync(x=> x.Name == name);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.TblAppRole
                .CountAsync(x => x.Name == name) > 0;
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludeId)
        {
            return await _context.TblAppRole
                .CountAsync(x => x.Name == name && x.Id != excludeId) > 0;
        }

        public async Task AddAsync(ApplicationRole role)
        {
            await _context.TblAppRole.AddAsync(role);
        }

        public async Task Update(ApplicationRole role)
        {
            _context.TblAppRole.Update(role);
        }

        public void Delete(ApplicationRole role)
        {
            _context.TblAppRole.Remove(role);
        }

        public async Task<bool> HasUsersAsync(int roleId)
        {
            return await _context.TblAppUserRole
                .CountAsync(x => x.RoleId == roleId) > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
