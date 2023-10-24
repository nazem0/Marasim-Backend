using System.ComponentModel.DataAnnotations;

namespace ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [Required, StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; } = false;
    }
}

