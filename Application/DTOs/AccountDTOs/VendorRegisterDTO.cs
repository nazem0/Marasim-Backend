using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.DTOs.AccountDTOs
{
    public class VendorRegisterDTO : IUserRegister
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }
        public string? PicUrl { get; set; }
        [Required, OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public IFormFile Picture { get; set; }
        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(15, MinimumLength = 11)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(20, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public required string Summary { get; set; }
        [AllowNull]
        public string Street { get; set; } = string.Empty;
        public required int CityId { get; set; }
        public required int GovernorateId { get; set; }
        public required string District { get; set; }
        public required bool Gender { get; set; }
        public required int CategoryId { get; set; }
        public string? ExternalUrl { get; set; }
    }
}
