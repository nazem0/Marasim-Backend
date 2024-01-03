using Application.DTOs.PostDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class PostExtension
    {
        public static Post ToEntity(this CreatePostDTO createPostDTO, int loggedInVendorId)
        {
            return new Post
            {
                VendorId = loggedInVendorId,
                Title = createPostDTO.Title,
                Description = createPostDTO.Description,
                DateTime = DateTime.Now
            };
        }

        public static PostDTO ToPostDTO(this Post post)
        {
            return new PostDTO
            {
                Id = post.Id,
                VendorId = post.VendorId,
                Title = post.Title,
                Description = post.Description,
                DateTime = post.DateTime,
                CommentsCount = post.Comments.Count,
                ReactsCount = post.Reacts.Count,
                PostAttachments = post.PostAttachments.Select(pa => pa.ToPostAttachmentDTO()),
                VendorName = post.Vendor.User.Name,
                VendorPicUrl = post.Vendor.User.PicUrl,
                VendorUserId = post.Vendor.User.Id
            };
        }
    }
}

