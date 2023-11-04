using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    public class PostAttachmentController : ControllerBase
    {
        private readonly PostAttachmentManager PostAttachmentManager;
        public PostAttachmentController(PostAttachmentManager _PostAttachmentManager)
        {
            PostAttachmentManager = _PostAttachmentManager;
        }
        public IActionResult GetPostAttachmentByPostID(int PostID)
        {
            var Data = PostAttachmentManager.Get().Where(sa => sa.PostID == PostID);
            return new JsonResult(Data);
        }
    }
}

