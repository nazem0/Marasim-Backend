using Microsoft.AspNetCore.Http;

namespace Application.DTOs.AccountDTOs
{
    public interface IUserRegister
    {
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string? PicUrl { get; set; }
        public IFormFile Picture { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Gender { get; set; }

    }
}
