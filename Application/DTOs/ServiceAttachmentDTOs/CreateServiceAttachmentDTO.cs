using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ServiceAttachmentDTOs
{
    public class CreateServiceAttachmentDTO
    {
        public required int ServiceId { get; set; }
        [Required, OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFileCollection Attachments { get; set; }
    }
}
