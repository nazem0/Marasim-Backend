using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using ViewModels.PostViewModels;

namespace API.Controllers
{
    public class PostController : ControllerBase
    {
        private PostManager PostManager { get; set; }
        private PostAttachmentManager PostAttachmentManager { get; set; }
        private VendorManager VendorManager { get; set; }
        private UserManager<User> UserManager { get; set; }
        public PostController
            (PostManager _PostManager,
            PostAttachmentManager _PostAttachmentManager,
            VendorManager _VendorManager,
            UserManager<User> _UserManager)
        {
            PostManager = _PostManager;
            VendorManager = _VendorManager;
            PostAttachmentManager = _PostAttachmentManager;
            UserManager = _UserManager;
        }
        public IActionResult Get()
        {
            var Data = PostManager.Get().Where(p => p.IsDeleted == false).ToList();
            return new JsonResult(Data);
        }

        public IActionResult GetPostByID(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            return new JsonResult(Data);
        }

        public IActionResult GetByVendorID(int VendorID)
        {
            var Data = PostManager.GetByVendorID(VendorID);
            return new JsonResult(Data);
        }

        [Authorize(Roles = "vendor")]
        public IActionResult AddPost([FromForm] AddPostViewModel Data)
        {
            if (ModelState.IsValid)
            {
                int VendorID = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                Post? NewPost = PostManager.Add(Data.ToModel(VendorID)).Entity;
                PostManager.Save();
                foreach (FormFile item in Data.Pictures)
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
                return Ok("Added");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "vendor,admin")]
        [HttpPost("{PostID:int}")]
        public IActionResult SoftDeletePost(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.IsDeleted = true;
            PostManager.Update(Data);
            PostManager.Save();
            return Ok("Deleted");
        }

        [Authorize(Roles = "vendor,admin")]
        public IActionResult UpdatePost(int PostID, [FromForm]EditPostViewModel OldPost)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.Title = OldPost.Title ?? Data.Title;
            Data.Description = OldPost.Description ?? Data.Description;
            Data.DateTime = OldPost.DateTime;
            Data.ServiceID = OldPost.ServiceID?? Data.ServiceID;

            PostManager.Update(Data);
            PostManager.Save();
            return Ok("Updated");
        }
    }
}

