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
        public IActionResult GetPostAttachmentByPostId(int PostId)
        {
            var Data = PostAttachmentManager.Get().Where(sa => sa.PostId == PostId);
            return new JsonResult(Data);
        }
    }
}

