using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.PostViewModels
{
    public class EditPostViewModel
    {
        [StringLength(100, MinimumLength = 5)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? Description { get; set; }

        public required DateTime DateTime { get; set; } = DateTime.Now;

        public int? ServiceID { get; set; }

        public required IFormFileCollection Pictures { get; set; }
    }
}

