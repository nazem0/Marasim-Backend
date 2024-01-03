using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PostAttachmentDTOs
{
    public class CreatePostAttachmentDTO
    {
        public required int PostId { get; set; }
        [Required,OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFileCollection Attachments { get; set; }
    }
}
