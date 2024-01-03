using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ServiceDTOs
{
    public class UpdateServiceDTO
    {
        [Required]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 20)]
        public string? Description { get; set; }

        public float? Price { get; set; }
    }
}
