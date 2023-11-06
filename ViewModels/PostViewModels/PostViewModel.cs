﻿using Models;

namespace ViewModels.PostViewModels
{
    public class PostViewModel
    {
        public required int Id { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DateTime { get; set; }
        public virtual required ICollection<PostAttachment> PostAttachments { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<React>? Reacts { get; set; }
        public required string VendorName { get; set; }
        public required string VendorPicUrl { get; set; }
        public required string VendorUserId { get; set; }
    }
}

