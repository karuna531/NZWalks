using NXWalks.Api.Models.Domain;

namespace NXWalks.Api.Models.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDifficultyId { get; set; }
        //NavigationProperty 
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
