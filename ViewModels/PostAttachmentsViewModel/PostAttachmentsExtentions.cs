using Models;
using ViewModels.PostAttachmentsViewModel;

namespace ViewModels.PostAttachmentsViewModel
{
    public static class PostAttachmentsExtentions
    {
        public static PostAttachmentViewModel ToViewModel(this PostAttachment PostAttachment)
        {
            return new PostAttachmentViewModel
            {
                Id = PostAttachment.Id,
                PostId = PostAttachment.PostId,
                AttachmentUrl = PostAttachment.AttachmentUrl
            };
        }

        public static PostAttachmentCustomViewModel ToCustomModel(this PostAttachment PostAttachment)
        {
            return new PostAttachmentCustomViewModel
            {
                Id = PostAttachment.Id,
                PostId = PostAttachment.PostId,
                AttachmentUrl = PostAttachment.AttachmentUrl,
                UserId = PostAttachment.Post.Vendor.UserId,
                VendorId = PostAttachment.Post.VendorId
            };
        }
    }
}

