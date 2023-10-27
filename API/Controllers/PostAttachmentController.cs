using System;
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
        public IActionResult Index()
        {
            var Data = PostAttachmentManager.Get().ToList();
            return new JsonResult(Data);
        }
    }
}

