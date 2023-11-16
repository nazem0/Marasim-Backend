using Microsoft.AspNetCore.Http;
using Models;
using System.Diagnostics.CodeAnalysis;

namespace ViewModels.VendorViewModels
{
    public class VendorRegistrationViewModel : IUserRegisteration
    {
        public required string Summary { get; set; }
        [AllowNull]
        public decimal? Latitude { get; set; } = default;
        [AllowNull]
        public decimal? Longitude { get; set; } = default;
        public string Street { get; set; } = string.Empty;
        public required int CityId { get; set; }
        public required int GovernorateId { get; set; }
        public required string District { get; set; }
        public required string Name { get; set; }
        public required string NationalId { get; set; }
        public string PicUrl { get; set; } = string.Empty;
        public required IFormFile Picture { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required bool Gender { get; set; }
        public required int CategoryId { get; set; }
        public string? ExternalUrl { get; set; }
    }
}
