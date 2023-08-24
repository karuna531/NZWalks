using Microsoft.EntityFrameworkCore;
using NXWalks.Api.Data;
using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {private readonly NzWalksDbContext _nzWalksDbContext;
        public RegionRepository(NzWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        public  async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await  _nzWalksDbContext.Regions.ToListAsync();

        }
    }
}
