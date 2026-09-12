using APARTMENT_API.Configurations;
using APARTMENT_API.DTOs.Request;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthorizationRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public async Task<User?> Login(AuthReqDto req)
        {
            var username = req.Username?.Trim();
            var password = req.Password?.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // Try database-level case-insensitive username match (ToUpper) and exact password
            try
            {
                var unameUpper = username.ToUpperInvariant();
                var exact = await _context.TblUser.FirstOrDefaultAsync(x =>
                    x.Active == true &&
                    x.Username != null &&
                    x.Password == password &&
                    x.Username.ToUpper() == unameUpper
                );
                if (exact != null)
                    return exact;
            }
            catch
            {
                // Fall back to in-memory comparison if translation to SQL failed
            }

            // Fallback: load candidates and compare in-memory using case-insensitive username
            var users = await _context.TblUser.Where(x => x.Active == true).ToListAsync();
            var match = users.FirstOrDefault(u =>
                string.Equals(u.Username?.Trim(), username, StringComparison.OrdinalIgnoreCase)
                && string.Equals(u.Password?.Trim(), password, StringComparison.Ordinal)
            );
            return match;
        }
    }
}
