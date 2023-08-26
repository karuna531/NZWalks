using Microsoft.EntityFrameworkCore;
using NXWalks.Api.Data;
using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories.WalkDifficultyRepository
{

    public class WalkDifficultRepository : IWalkDifficultRepository
    {
        private readonly NzWalksDbContext _nzWalkDbContext;
        public WalkDifficultRepository(NzWalksDbContext nzWalkDbCOntext)
        {
            _nzWalkDbContext = nzWalkDbCOntext;
            
        }
        public async Task<WalkDifficulty> CreateWalkDifficulty(WalkDifficulty walk)
        {
            walk.Id = Guid.NewGuid();
            await _nzWalkDbContext.WalkDifficulties.AddAsync(walk);
            await _nzWalkDbContext.SaveChangesAsync();
            return walk;    


        }

        public async Task<WalkDifficulty> DeleteWalkDIfficulty(Guid id)
        {
             var walkDiffucultyData = await _nzWalkDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id); 
            if(walkDiffucultyData == null)
            {
                return null;
            }
            _nzWalkDbContext.WalkDifficulties.Remove(walkDiffucultyData);
            _nzWalkDbContext.SaveChangesAsync();
            return walkDiffucultyData;


        }

        public async  Task<IEnumerable<WalkDifficulty>> GellAllWalkDifficulty()
        {
            return await _nzWalkDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyById(Guid id)
        {
            return await _nzWalkDbContext.WalkDifficulties.FirstOrDefaultAsync(x=>x.Id == id);
            
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walk)
        {
            var walkData = await _nzWalkDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (walkData == null)
            {
                return null;
            }

            walkData.code = walk.code; 

            await _nzWalkDbContext.SaveChangesAsync(); 

            return walkData; 
        }

    }
}
