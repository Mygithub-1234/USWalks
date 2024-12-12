using System.Diagnostics.Eventing.Reader;
using USWalks.SPI.Models.Domain;

namespace USWalks.SPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync(int pageNumber, int pageSize, bool? isAscending, string? filterOn = null, string? filterQuery = null, string? sortBy = null);
            Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);

    }
}
