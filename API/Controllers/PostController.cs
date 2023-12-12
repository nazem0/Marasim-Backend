using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.PostAttachmentsViewModel;
using ViewModels.PostViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostRepository PostManager;
        private readonly PostAttachmentRepository PostAttachmentManager;
        private readonly VendorRepository VendorManager;
        public PostController
            (PostRepository _PostManager,
            PostAttachmentRepository _PostAttachmentManager,
            VendorRepository _VendorManager)
        {
            PostManager = _PostManager;
            VendorManager = _VendorManager;
            PostAttachmentManager = _PostAttachmentManager;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var Data = PostManager.Get()
                .Select(p => p.ToViewModel());
            return Ok(Data);
        }

        [HttpGet("GetById/{PostId}")]
        public IActionResult GetById(int PostId)
        {
            PostViewModel? Post = PostManager.GetById(PostId);
            if (Post is null)
                return NotFound();
            return Ok(Post);
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId, int PageSize = 2, int PageIndex = 1)
        {
            var Data = PostManager.GetByVendorId(VendorId, PageSize, PageIndex);
            return Ok(Data);
        }

        [HttpGet("GetByPostsByFollow")]
        public IActionResult GetByPostsByFollow(int PageSize = 2, int PageIndex = 1)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier!)!;
            var Data = PostManager.GetByPostsByFollow(UserId, PageSize, PageIndex);
            return Ok(Data);
        }

        [Authorize(Roles = "vendor")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddPostViewModel Data)
        {
            if (ModelState.IsValid)
            {
                int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                if (_vendorId is null)
                    return Unauthorized();
                int VendorId = (int)_vendorId;
                Post? NewPost = PostManager.Add(Data.ToModel(VendorId)).Entity;
                PostManager.Save();
                foreach (IFormFile item in Data.Pictures)
                {
                    FileInfo fi = new(item.FileName);
                    string FileName = DateTime.Now.Ticks + fi.Extension;
                    Helper.UploadMediaAsync
                        (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                        , "PostAttachment", FileName, item, $"{NewPost.Id}-{NewPost.VendorId}");
                    PostAttachmentManager.Add(
                        new PostAttachment
                        {
                            AttachmentUrl = FileName,
                            Post = NewPost
                        }
                        );
                }
                PostManager.Save();
                return Ok();
            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);
            }
        }

        [Authorize(Roles = "vendor,admin")]
        [HttpDelete("Delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            int? PostVendorId = PostManager.Get(PostId)?.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (PostVendorId != null && PostVendorId == LoggedInVendorId)
            {
                PostManager.Delete(PostId);
                PostManager.Save();
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = "vendor")]
        [HttpPut("Update/{PostId}")]
        public IActionResult Update(int PostId, [FromForm] EditPostViewModel OldPost)
        {
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? PostVendorId = PostManager.Get(PostId)?.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId(LoggedInUserId);
            Post? Post = PostManager.Get(PostId);
            if (!ModelState.IsValid || Post == null)
            {
                return BadRequest("Post Invalid");
            }
            else if (PostVendorId != LoggedInVendorId)
            {
                return Unauthorized();
            }
            else
            {
                Post.Title = OldPost.Title ?? Post.Title;
                Post.Description = OldPost.Description ?? Post.Description;
                PostManager.Update(Post);
                PostManager.Save();
                return Ok();
            }

        }
    }
}

