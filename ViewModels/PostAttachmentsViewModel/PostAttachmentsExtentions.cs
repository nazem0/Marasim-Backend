using Models;
using ViewModels.UserViewModels;

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
                ID = PostAttachment.ID,
                PostID = PostAttachment.PostID,
                AttachmentUrl = PostAttachment.AttachmentUrl
            };
        }
    }
}

