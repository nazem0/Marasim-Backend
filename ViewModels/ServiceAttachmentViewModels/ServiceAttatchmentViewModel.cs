using Models;

namespace ViewModels.ServiceAttachmentViewModels
{
    public class ServiceAttachmentViewModel : BaseModel
    {
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}
