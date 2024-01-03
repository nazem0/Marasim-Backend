using Application.DTOs.PostAttachmentDTOs;

namespace Application.DTOs.PostDTOs
{
    public class PostDTO
    {
        public required int Id { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime DateTime { get; set; }
        public required IEnumerable<PostAttachmentDTO> PostAttachments { get; set; }
        public int CommentsCount { get; set; }
        public int ReactsCount { get; set; }
        public required string VendorName { get; set; }
        public required string VendorPicUrl { get; set; }
        public required string VendorUserId { get; set; }
    }
}

