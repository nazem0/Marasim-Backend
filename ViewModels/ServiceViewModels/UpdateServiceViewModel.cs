using System.ComponentModel.DataAnnotations;

namespace ViewModels.ServiceViewModels
{
    public class UpdateServiceViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }
        [Required, MaxLength(1000)]
        public string? Description { get; set; }
        public float? Price { get; set; }

    }
}
