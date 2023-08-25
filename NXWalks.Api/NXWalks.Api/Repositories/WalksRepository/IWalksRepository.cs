using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories.WalksRepository
{
    public interface IWalksRepository
    {
        Task <IEnumerable<Walk>> GetAllWalks();
        Task<Walk> GetWalkById (Guid Id);
        Task<Walk> CreateWalk(Walk walk);
        Task<Walk> UpdateWalk(Guid Id,Walk walk);
        Task<Walk> DeleteWalk(Guid Id);
    }
}
