using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.PostViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostManager PostManager;
        private readonly PostAttachmentManager PostAttachmentManager;
        private readonly VendorManager VendorManager;
        public PostController
            (PostManager _PostManager,
            PostAttachmentManager _PostAttachmentManager,
            VendorManager _VendorManager)
        {
            PostManager = _PostManager;
            VendorManager = _VendorManager;
            PostAttachmentManager = _PostAttachmentManager;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var Data = PostManager.Get()
                .Include(p => p.PostAttachments)
                .Include(p => p.Comments)
                .Include(p => p.Reacts)
                .Select(p => p.ToViewModel(p.Vendor.User));
            return Ok(Data);
        }

        [HttpGet("GetPostByID/{PostID}")]
        public IActionResult GetPostByID(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            return new JsonResult(Data);
        }

        [HttpGet("GetByVendorID/{VendorID}")]
        public IActionResult GetByVendorID(int VendorID)
        {
            var Data = PostManager.GetByVendorID(VendorID)
                .Include(p => p.PostAttachments)
                .Include(p => p.Comments)
                .Include(p => p.Reacts)
                .Select(p => p.ToViewModel(p.Vendor.User));
            return Ok(Data);
        }

        [Authorize(Roles = "vendor")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddPostViewModel Data)
        {
            if (ModelState.IsValid)
            {
                int VendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                Post? NewPost = PostManager.Add(Data.ToModel(VendorId)).Entity;
                PostManager.Save();
                foreach (IFormFile item in Data.Pictures)
                {
                    FileInfo fi = new(item.FileName);
                    string FileName = DateTime.Now.Ticks + fi.Extension;
                    Helper.UploadMediaAsync
                        (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                        , "PostAttachment", FileName, item, $"{NewPost.Id}-{NewPost.Title}");
                    PostAttachmentManager.Add(
                        new PostAttachment
                        {
                            AttachmentUrl = FileName,
                            Post = NewPost
                        }
                        );
                }
                PostAttachmentManager.Save();
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
            int? PostVendorId = PostManager.Get(PostId)!.FirstOrDefault()?.VendorId;
            int? LoggedInVendorId = VendorManager.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (PostVendorId != null && PostVendorId == LoggedInVendorId )
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

        [Authorize(Roles = "vendor,admin")]
        [HttpPut("Update/{PostID}")]
        public IActionResult Update(int PostID, [FromForm] EditPostViewModel OldPost)
        {
            Post? Post = PostManager.GetPostByID(PostID);
            if (Post is not null)
            {
                Post.Title = OldPost.Title ?? Post.Title;
                Post.Description = OldPost.Description ?? Post.Description;
                Post.DateTime = OldPost.DateTime;
                PostManager.Update(Post);
                PostManager.Save();
                return Ok("Updated");
            }
            else
            {
                return NotFound();
            }
        }
    }
}

