using Models;

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

        public static PostViewModel ToViewModel(this Post Post, User User)
        {
            return new PostViewModel
            {
                Id = Post.Id,
                VendorId = Post.VendorId,
                Title = Post.Title,
                Description = Post.Description,
                DateTime = Post.DateTime,
                Comments = Post.Comments,
                Reacts = Post.Reacts,
                PostAttachments = Post.PostAttachments,
                VendorName = User.Name,
                VendorPicUrl = User.PicUrl,
                VendorUserId = User.Id
            };
        }
    }
}

