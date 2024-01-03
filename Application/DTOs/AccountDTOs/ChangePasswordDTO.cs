using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AccountDTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public required string OldPassword { get; set; }
        [Required]
        public required string NewPassword { get; set; }
        [Required, Compare("NewPassword")]
        public required string ConfirmPassword { get; set; }
    }
}
