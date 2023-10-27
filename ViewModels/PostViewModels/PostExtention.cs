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
        public static Post ToModel(this AddPostViewModel AddPost, int VendorID)
        {
            return new Post
            {
                VendorID = VendorID,
                Title = AddPost.Title,
                ServiceID = AddPost.ServiceID,
                Description = AddPost.Description,
                DateTime = AddPost.DateTime
            };
        }
    }
}

