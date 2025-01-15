namespace Application.DTOs.ServiceAttachmentDTOs
{
    public class ServiceAttachmentDTO
    {
        public required int Id { get; set; }
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}
