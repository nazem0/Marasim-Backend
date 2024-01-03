using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ServiceDTOs
{
    public class CreateServiceDTO
    {
        [Required, MaxLength(100)]
        public required string Title { get; set; }
        [Required, MaxLength(1000)]
        public required string Description { get; set; }
        [Required]
        public required float Price { get; set; }
        [Required,OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFileCollection Pictures { get; set; }
    }
}
