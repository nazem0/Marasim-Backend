using Models;

namespace ViewModels.ServiceAttatchmentViewModels
{
    public class ServiceAttatchmentViewModel : BaseModel
    {
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}
