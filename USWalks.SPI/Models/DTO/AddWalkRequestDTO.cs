using System.ComponentModel.DataAnnotations;

namespace USWalks.SPI.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        //Navigation properties
        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
