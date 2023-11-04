using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.PostViewModels;

namespace API.Controllers
{
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
        public IActionResult Get()
        {
            var Data = PostManager.Get().Where(p => p.IsDeleted == false)
                .AsNoTracking()
                .Include(p => p.PostAttachments)
                .Include(p => p.Comments)
                .Include(p => p.Reacts)
                .Select(p => p.ToViewModel(p.Vendor.User));
            return Ok(Data);
        }

        public IActionResult GetPostByID(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            return new JsonResult(Data);
        }

        public IActionResult GetByVendorID(int VendorID)
        {
            var Data = PostManager.GetByVendorID(VendorID)
                .AsNoTracking()
                .Include(p => p.PostAttachments)
                .Include(p => p.Comments)
                .Include(p => p.Reacts);
            return Ok(Data);
        }

        [Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] AddPostViewModel Data)
        {
            if (ModelState.IsValid)
            {
                int VendorID = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                Post? NewPost = PostManager.Add(Data.ToModel(VendorID)).Entity;
                PostManager.Save();
                foreach (IFormFile item in Data.Pictures)
                {
                    FileInfo fi = new(item.FileName);
                    string FileName = DateTime.Now.Ticks + fi.Extension;
                    Helper.UploadMediaAsync
                        (User.FindFirstValue(ClaimTypes.NameIdentifier)!
                        , "PostAttachment", FileName, item, $"{NewPost.ID}-{NewPost.Title}");
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
        public IActionResult Delete(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.IsDeleted = true;
            PostManager.Update(Data);
            PostManager.Save();
            return Ok("Deleted");
        }

        [Authorize(Roles = "vendor,admin")]
        public IActionResult Update(int PostID, [FromForm] EditPostViewModel OldPost)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.Title = OldPost.Title ?? Data.Title;
            Data.Description = OldPost.Description ?? Data.Description;
            Data.DateTime = OldPost.DateTime;
            Data.ServiceID = OldPost.ServiceID ?? Data.ServiceID;

            PostManager.Update(Data);
            PostManager.Save();
            return Ok("Updated");
        }
    }
}

