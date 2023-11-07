using System;
using Models;

namespace ViewModels.PostViewModels
{
	public class PostPartialViewModel
	{
        public required int Id { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DateTime { get; set; }
        public required IEnumerable<PostAttachmentViewModel> PostAttachments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<React>? Reacts { get; set; }
    }
}

