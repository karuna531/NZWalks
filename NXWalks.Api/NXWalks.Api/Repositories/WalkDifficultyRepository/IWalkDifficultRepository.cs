using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories.WalkDifficultyRepository
{
    public interface IWalkDifficultRepository
    {
          Task<IEnumerable<WalkDifficulty>> GellAllWalkDifficulty();
          Task<WalkDifficulty> GetWalkDifficultyById(Guid id);
          Task<WalkDifficulty> CreateWalkDifficulty(WalkDifficulty walk);
         Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walk);
        Task<WalkDifficulty> DeleteWalkDIfficulty(Guid id);


    }
}
