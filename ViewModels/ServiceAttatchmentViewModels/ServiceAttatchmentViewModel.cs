using Models;

namespace ViewModels.ServiceAttatchmentViewModels
{
    public class ServiceAttatchmentViewModel : BaseModel
    {
        public required int ServiceID { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}
