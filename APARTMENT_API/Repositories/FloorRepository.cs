using APARTMENT_API.Configurations;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APARTMENT_API.Repositories
{
    public class FloorRepository : IFloorRepository
    {
        private readonly ApplicationDbContext _context;
        // Constructor For Repository
        public FloorRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PageResult<Floor>> GetFloorAsynce(int page = 1, int pageSize = 10)
        {
            var data = await _context.TblFloor.ToPagedListAsync(page, pageSize);
            return data;
        }
        public async Task<List<Floor>>  GetAllFloorAsynce(){
            var data = await _context.TblFloor.ToListAsync();
            return data;
        }
        public async Task<Floor> GetFloorByIdAsynce(int floorId)
        {
            // SELECT * FROM TBLFLOOR WHERE ID=FLOORID;
            var data = await _context.TblFloor.FirstOrDefaultAsync(f=> f.Id==floorId);
            return data!;
        }
        public async Task<Floor> CreateAsynce(Floor req)
        {
            await _context.TblFloor.AddAsync(req);
            await _context.SaveChangesAsync();
            return req;
        }
        public async Task<Floor> UpdateAsynce(Floor req)
        {
            _context.TblFloor.Update(req);
            await _context.SaveChangesAsync();
            return req;
        }

        public async Task<bool> DeleteAsynce(Floor req)
        {
            _context.TblFloor.Remove(req);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
