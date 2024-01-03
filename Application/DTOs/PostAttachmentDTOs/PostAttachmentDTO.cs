namespace Application.DTOs.PostAttachmentDTOs
{
    public class PostAttachmentDTO
    {
        public int Id { get; set; }
        public required int PostId { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}

