using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Models;

namespace ViewModels.PostViewModels
{
    public static class PostExtention
    {
        public static Post ToModel(this AddPostViewModel AddPost)
        {
            var _PostAttachments = new List<PostAttachment>();
            var _Comments = new List<Comment>();
            var _Reacts = new List<React>();

            foreach (var item in AddPost.PostAttachments)
            {
                _PostAttachments.Add(new PostAttachment()
                {
                    AttachmentUrl = item.AttachmentUrl,
                });
            }
            
            return new Post
            {
                VendorID = AddPost.VendorID,
                Title = AddPost.Title,
                ServiceID = AddPost.ServiceID,
                Description = AddPost.Description,
                PostAttachments = _PostAttachments,
            };
        }

        public static AddPostViewModel ToAddPostViewModel(this Post Post)
        {
            return new AddPostViewModel()
            {
                ID = Post.ID,
                VendorID = Post.VendorID,
                Title = Post.Title,
                DateTime = DateTime.Now,
                ServiceID = Post.ServiceID,
                Description = Post.Description,
                PostAttachments = Post.PostAttachments.ToList(),
                Comments = Post.Comments.ToList(),
                Reacts = Post.Reacts.ToList()
            };
        }
        
    }
}

