using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NXWalks.Api.Data;
using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Repositories.WalksRepository
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NzWalksDbContext _nzWalksDbContext;
        public WalksRepository(NzWalksDbContext nzWalksDbContext)
        {
            _nzWalksDbContext = nzWalksDbContext;
        }
        public async Task<Walk> CreateWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _nzWalksDbContext.Walks.AddAsync(walk);
            await _nzWalksDbContext.SaveChangesAsync();
            return walk;
            
        }

        public async Task<Walk> DeleteWalk(Guid Id)
        {
            var walk =await  _nzWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (walk == null)
            {
                return null;
            }
             _nzWalksDbContext.Walks.Remove(walk);
            _nzWalksDbContext.SaveChanges();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            return await _nzWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }


        public async Task<Walk> GetWalkById(Guid Id)
        {
           var walk = await _nzWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == Id);
        
            return walk;
        }

        public async Task<Walk> UpdateWalk(Guid Id, Walk updatedwalk)
        {
          var walkData = await _nzWalksDbContext.Walks.FirstOrDefaultAsync(x=>x.Id == Id);
           if(walkData == null)
            {
                return null;
            }
           walkData.Name = updatedwalk.Name;
            walkData.Length = updatedwalk.Length;
            await _nzWalksDbContext.SaveChangesAsync();

            return walkData;

        }
    }
}
