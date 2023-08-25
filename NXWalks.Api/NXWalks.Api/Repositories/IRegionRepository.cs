using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories
{
    public interface IRegionRepository
    {
      Task <IEnumerable<Region>> GetAllAsync();
       Task<Region> GetById(Guid id);
        Task<Region> AddRegion(Region region);
         Task<Region> DeleteRegion(Guid Id);
        Task<Region> UpdateRegion(Guid Id, Region region);

    }
}
