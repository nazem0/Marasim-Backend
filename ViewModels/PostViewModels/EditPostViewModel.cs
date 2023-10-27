using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.PostViewModels
{
	public class EditPostViewModel
	{
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required, StringLength(1000, MinimumLength = 10)]
        public required string Description { get; set; }

        public required DateTime DateTime { get; set; } = DateTime.Now;

        public int? ServiceID { get; set; }

        public required IFormFileCollection Pictures { get; set; }
    }
}

