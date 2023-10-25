using System;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    public class PostController : ControllerBase
    {
        private PostManager PostManager { get; set; }
        public PostController(PostManager _PostManager)
        {
            PostManager = _PostManager;
        }
        public IActionResult GetPosts()
        {
            var Data = PostManager.Get().ToList();
            return new JsonResult(Data);
        }
    }
}

