using Models;
using ViewModels.UserViewModels;

namespace ViewModels.PostViewModels
{
    public class PostAttachmentViewModel : BaseModel
    {
        public required int PostID { get; set; }
        public required string AttachmentUrl { get; set; }
    }
}

