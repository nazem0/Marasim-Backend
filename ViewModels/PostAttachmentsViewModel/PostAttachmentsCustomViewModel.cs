namespace ViewModels.PostAttachmentsViewModel
{
	public class PostAttachmentCustomViewModel
	{
        public required int Id { get; set; }
        public required int PostId { get; set; }
        public required string AttachmentUrl { get; set; }
        public required string UserId { get; set; }
        public required int VendorId { get; set; }
    }
}

