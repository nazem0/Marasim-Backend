namespace ViewModels.ServiceAttachmentViewModels
{
    public class ServiceAttachmentCustomViewModel
    {
        public required int Id { get; set; }
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
        public required string UserId { get; set; }
        public required int VendorId { get; set; }
    }
}


