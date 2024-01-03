using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VendorDTOs
{
    public class UpdateVendorDTO
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        public string? PicURL { get; set; }
        [OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public IFormFile? Picture { get; set; }

        [StringLength(15, MinimumLength = 11)]
        public string? PhoneNumber { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(1000, MinimumLength = 20)]
        public string? Summary { get; set; }
    }
}
