using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    public class PostAttachmentController : ControllerBase
    {
        private PostAttachmentManager PostAttachmentManager { get; set; }
        public PostAttachmentController(PostAttachmentManager _PostAttachmentManager)
        {
            PostAttachmentManager = _PostAttachmentManager;
        }
        public IActionResult GetPostAttachmentByPostID(int PostID)
        {
            var Data = PostAttachmentManager.GetPostAttachmentByPostID(PostID);
            return new JsonResult(Data);
        }
    }
}

