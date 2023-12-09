using System.ComponentModel.DataAnnotations;

namespace ViewModels.UserViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public required string OldPassword { get; set; }
        [Required]
        public required string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public required string ConfirmPassword { get; set; }
    }
}
