namespace Application.DTOs.ServiceAttachmentsDTOs
{
    public class IndependentServiceAttachmentDTO
    {
        public required int Id { get; set; }
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
        public required string UserId { get; set; }
        public required int VendorId { get; set; }
    }
}


