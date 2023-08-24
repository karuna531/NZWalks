using Microsoft.EntityFrameworkCore;
using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Data
{
    public class NzWalksDbContext:DbContext
    {
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> options): base(options)
        {
            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }



    }
}
