using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReviewViewModels
{
	public class UpdateReviewViewModel
	{
        [Required]
        public required string UserID { get; set; }

        [Required]
        public required int Rate { get; set; }

        [Required, StringLength(1000, MinimumLength = 10)]
        public required string Message { get; set; }
    }
}

