using Models;

namespace ViewModels.PostViewModels
{
    public class PostAttachmentViewModel : BaseModel
    {
        public required int PostId { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}

