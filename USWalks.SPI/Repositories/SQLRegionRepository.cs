using Microsoft.EntityFrameworkCore;
using USWalks.SPI.Data;
using USWalks.SPI.Models.Domain;
using USWalks.SPI.Repositories;

namespace USWalks.SPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly USWalksDbContext _context;
        public SQLRegionRepository(USWalksDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            _context.Regions.Remove(existingRegion);
            await _context.SaveChangesAsync();

            return existingRegion;

        }

        public async Task<List<Region>> GetAllAsync(int pageNumber, int pageSize, bool? isAscending = true,  string? filterOn = null, string? filterQuery = null, string? sortBy = null)
        {
            var regions = _context.Regions.AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = regions.Where(x => x.Name.Contains(filterQuery));
                }

            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.CurrentCultureIgnoreCase))
                {
                    regions = (bool)isAscending ? regions.OrderBy(x => x.Name) : regions.OrderByDescending(x => x.Name);
                }
            }
            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            //
            return await regions.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();
            return existingRegion;
        }
    }
}
