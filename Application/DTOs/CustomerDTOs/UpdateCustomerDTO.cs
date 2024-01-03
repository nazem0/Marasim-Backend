using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CustomerDTOs
{
    public class UpdateCustomerDTO
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; } = null;

        [StringLength(14, MinimumLength = 14)]
        public string? PicURL { get; set; } = null;
        [OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public IFormFile? Picture { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; } = null;
    }
}
