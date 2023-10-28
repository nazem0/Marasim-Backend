using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReviewViewModels
{
	public class AddReviewViewModel
	{
        [Required]
        public required string UserID { get; set; }

        [Required]
        public required int ServiceID { get; set; }

        [Required]
        public required int Rate { get; set; }

        [Required, StringLength(1000, MinimumLength = 10)]
        public required string Message { get; set; }

        [Required]
        public required DateTime DateTime { get; set; } = DateTime.Now;
    }
}

