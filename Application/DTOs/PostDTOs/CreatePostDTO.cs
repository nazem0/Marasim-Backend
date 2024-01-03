using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PostDTOs
{
    public class CreatePostDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required, StringLength(1000, MinimumLength = 10)]
        public required string Description { get; set; }

        [Required,OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFileCollection Pictures { get; set; }
    }
}

