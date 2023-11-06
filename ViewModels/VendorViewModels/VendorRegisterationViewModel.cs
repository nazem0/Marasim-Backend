using Microsoft.AspNetCore.Http;
using Models;


namespace ViewModels.VendorViewModels
{
    public class VendorRegisterationViewModel : IUserRegisteration
    {
        public required string Summary { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public required string Address { get; set; }
        public required string Name { get; set; }
        public required string NationalID { get; set; }
        public string PicUrl { get; set; } = string.Empty;
        public required IFormFile Picture { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required bool Gender { get; set; }
        public required int CategoryId { get; set; }
    }
}
