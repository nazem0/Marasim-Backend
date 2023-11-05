﻿using Models;

namespace ViewModels.PostViewModels
{
    public static class PostExtension
    {
        public static Post ToModel(this AddPostViewModel AddPost, int VendorID)
        {
            return new Post
            {
                VendorID = VendorID,
                Title = AddPost.Title,
                ServiceID = AddPost.ServiceID,
                Description = AddPost.Description,
                DateTime = DateTime.Now
            };
        }

        public static PostViewModel ToViewModel(this Post Post, User User)
        {
            return new PostViewModel
            {
                ID = Post.ID,
                VendorID = Post.VendorID,
                Title = Post.Title,
                Description = Post.Description,
                DateTime = Post.DateTime,
                ServiceID = Post.ServiceID,
                Comments = Post.Comments,
                Reacts = Post.Reacts,
                PostAttachments = Post.PostAttachments,
                VendorName = User.Name,
                VendorPicUrl = User.PicUrl,
                VendorUserID = User.Id
            };
        }
    }
}
