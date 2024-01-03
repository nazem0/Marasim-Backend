using Application.DTOs.PostAttachmentDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class PostAttachmentExtensions
    {
        public static PostAttachmentDTO ToPostAttachmentDTO(this PostAttachment postAttachment)
        {
            return new PostAttachmentDTO
            {
                Id = postAttachment.Id,
                PostId = postAttachment.PostId,
                AttachmentUrl = postAttachment.AttachmentUrl
            };
        }

        public static IndependentPostAttachmentDTO ToIndependentPostAttachmentDTO(this PostAttachment postAttachment)
        {
            return new IndependentPostAttachmentDTO
            {
                Id = postAttachment.Id,
                PostId = postAttachment.PostId,
                AttachmentUrl = postAttachment.AttachmentUrl,
                UserId = postAttachment.Post.Vendor.UserId,
                VendorId = postAttachment.Post.VendorId
            };
        }
    }
}

