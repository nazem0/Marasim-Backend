using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.PostViewModels
{
    public class AddPostViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required, StringLength(1000, MinimumLength = 10)]
        public required string Description { get; set; }

        public int? ServiceId { get; set; }

        [Required]
        public required IFormFileCollection Pictures { get; set; }
    }
}

