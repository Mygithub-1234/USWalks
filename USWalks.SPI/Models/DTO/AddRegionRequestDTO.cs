using System.ComponentModel.DataAnnotations;

namespace USWalks.SPI.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Length should be minimum 3 characters")]
        [MaxLength(3,ErrorMessage ="Length should be maximum 3 characters")]
        public string Code { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Length should be minimum 1 character")]
        [MaxLength(100, ErrorMessage = "Length should be maximum 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
