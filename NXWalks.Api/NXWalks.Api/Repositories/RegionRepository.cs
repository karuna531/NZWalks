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

        public async Task<Region> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nzWalksDbContext.AddAsync(region);
            await _nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid Id)
        {
            var region = await _nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (region == null)
            {
                return null;
            }
            //Delete Region
            _nzWalksDbContext.Regions.Remove(region);
            await _nzWalksDbContext.SaveChangesAsync();
            return region;

        }

        public  async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await  _nzWalksDbContext.Regions.ToListAsync();

        }

        public async Task<Region> GetById(Guid id)
        {
            var region =  await _nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> UpdateRegion( Guid Id ,Region region)
        {
            var regionData = await _nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if(regionData == null)
            {
                return null;
            }
            //return await _nzWalksDbContext.Regions.Update(regionData);
            regionData.Code = region.Code;
            regionData.Name = region.Name;
            regionData.Area = region.Area;
            regionData.Lat = region.Lat;
            regionData.Long = region.Long;
            regionData.Population = region.Population;
            await _nzWalksDbContext.SaveChangesAsync();
            return regionData;

        }
    }
}
