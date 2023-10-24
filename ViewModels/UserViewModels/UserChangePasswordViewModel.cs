using System.ComponentModel.DataAnnotations;

namespace ViewModels.UserViewModels
{
    public class UserChangePasswordViewModel
    {
        public required string ID { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
        [Compare("NewPassword")]
        public required string ConfirmPassword { get; set; }
    }
}
