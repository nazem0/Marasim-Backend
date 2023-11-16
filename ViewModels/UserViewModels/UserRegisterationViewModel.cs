using Microsoft.AspNetCore.Http;
using Models;

namespace ViewModels.UserViewModels
{
    public class UserRegistrationViewModel : IUserRegisteration
    {
        public required string Name { get; set; }
        public required string NationalId { get; set; }
        public string PicUrl { get; set; } = string.Empty;
        public required IFormFile Picture { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required bool Gender { get; set; }
    }
}

