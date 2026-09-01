using Microsoft.EntityFrameworkCore;
namespace APARTMENT_API.Helpers
{
    public static class QueryableExtensions
    {
        public static async Task<PageResult<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalRecords = await query.CountAsync();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageResult<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords/(double)pageSize),
                Data = data
            };
        }   
    }
}
