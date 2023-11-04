using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactsController : ControllerBase
    {
        private readonly PostManager PostManager;
        private readonly ReactsManager ReactsManager;
        private readonly VendorManager VendorManager;
        public ReactsController
            (PostManager _PostManager,
            ReactsManager _ReactsManager,
            VendorManager _VendorManager)
        {
            PostManager = _PostManager;
            ReactsManager = _ReactsManager;
            VendorManager = _VendorManager;
        }

        public IActionResult GetReactsByPostID(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID).Reacts;
            return new JsonResult(Data);
        }
    }
}
