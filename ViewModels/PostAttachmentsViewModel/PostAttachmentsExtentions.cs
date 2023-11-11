using Models;

namespace ViewModels.PostViewModels
{
    public static class PostAttachmentsExtentions
    {
        //public static Post ToModel(this AddPostViewModel AddPost, int VendorID)
        //{
        //    return new Post
        //    {
        //        VendorID = VendorID,
        //        Title = AddPost.Title,
        //        ServiceID = AddPost.ServiceID,
        //        Description = AddPost.Description,
        //        DateTime = AddPost.DateTime
        //    };
        //}

        public static PostAttachmentViewModel ToViewModel(this PostAttachment PostAttachment)
        {
            return new PostAttachmentViewModel
            {
                Id = PostAttachment.Id,
                PostId = PostAttachment.PostId,
                AttachmentUrl = PostAttachment.AttachmentUrl
            };
        }
    }
}

