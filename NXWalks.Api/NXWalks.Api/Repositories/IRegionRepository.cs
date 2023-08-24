using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories
{
    public interface IRegionRepository
    {
      Task< IEnumerable<Region>> GetAllAsync();
    }
}
