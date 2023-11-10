using System.ComponentModel.DataAnnotations;

namespace ViewModels.ServiceViewModels
{
    public class UpdateServiceViewModel
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 20)]
        public string? Description { get; set; }

        public float? Price { get; set; }
    }
}
