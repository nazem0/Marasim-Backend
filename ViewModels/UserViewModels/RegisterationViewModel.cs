using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.UserViewModels
{
	public class RegisterationViewModel
	{
        [Required, StringLength(50, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        public required string NationalID { get; set; }

        //[Required]
        //public required IFormFileCollection Photo { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required, StringLength(15, MinimumLength = 11)]
        public required string PhoneNumber { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        [Required]
        public required bool Gender { get; set; }

    }
}

