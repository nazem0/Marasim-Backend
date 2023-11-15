using Models;
using ViewModels.CommentViewModels;
using ViewModels.ReactViewModels;

namespace ViewModels.PostViewModels
{
    public static class PostExtension
    {
        public static Post ToModel(this AddPostViewModel AddPost, int VendorId)
        {
            return new Post
            {
                VendorId = VendorId,
                Title = AddPost.Title,
                Description = AddPost.Description,
                DateTime = DateTime.Now
            };
        }

        public static PostViewModel ToViewModel(this Post Post)
        {
            return new PostViewModel
            {
                Id = Post.Id,
                VendorId = Post.VendorId,
                Title = Post.Title,
                Description = Post.Description,
                DateTime = Post.DateTime,
                Comments = Post.Comments?.Select(c => c.ToViewModel(Post.Vendor.User)),
                Reacts = Post.Reacts?.Select(r => r.ToViewModel(Post.Vendor.User)),
                PostAttachments = Post.PostAttachments.Select(pa => pa.ToViewModel()),
                VendorName = Post.Vendor.User.Name,
                VendorPicUrl = Post.Vendor.User.PicUrl,
                VendorUserId = Post.Vendor.User.Id
            };
        }
    }
}

