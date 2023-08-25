namespace NXWalks.Api.Models.DTO
{
    public class UpdateWalk
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDifficultyId { get; set; }


    }
}
