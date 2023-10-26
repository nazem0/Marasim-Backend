using System;
using Models;

namespace ViewModels.PostViewModels
{
	public class PostViewModel
	{
        public required int VendorID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DateTime { get; set; }
        public int? ServiceID { get; set; } = null;
        public ICollection<PostAttachment>?  PostAttachments { get; set; }
        public ICollection<Comment>? Comment { get; set; }
        public ICollection<React>? React { get; set; }
    }
}

