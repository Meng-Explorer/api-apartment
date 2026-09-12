using APARTMENT_API.Configurations;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;      
        }
        public async Task<PageResult<ApplicationUser>> GetUserByPageAsync( int page = 1, int pageSize = 10)
        {
            var data = await _context.TblAppUser.ToPagedListAsync(page, pageSize);
            return data;
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            var data = await _context.TblAppUser.ToListAsync();
            return data;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int userId)
        {
            var data = await _context.TblAppUser
                .FirstOrDefaultAsync(x=> x.Id == userId);
            return data;
        } 

        public async Task<ApplicationUser?> GetUserByNameAsync(string username)
        {
            username = username.Trim().ToLowerInvariant();
            var data = await _context.TblAppUser
                .FirstOrDefaultAsync(x=> x.Username == username);
            return data;
        } 

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            var emailExists = await _context.TblAppUser
                .FirstOrDefaultAsync(x => x.Email == email.Trim().ToLowerInvariant());
            return emailExists;
        }

        public async Task<ApplicationUser?> Login(ApplicationUser request)
        {
            var data = await _context.TblAppUser
                .FirstOrDefaultAsync(x=> x.Username == request.Username && x.PasswordHash == request.PasswordHash);
            return data;
        }

        public async Task<ApplicationUser> Register(ApplicationUser request)
        {
            await _context.TblAppUser.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

    }
}

