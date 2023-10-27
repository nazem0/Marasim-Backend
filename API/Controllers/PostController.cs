﻿using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
            var Data = PostManager.Get().Where(p => p.IsDeleted == false).ToList();
            return new JsonResult(Data);
        }

        public IActionResult Get(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            return new JsonResult(Data);
        }

        [Authorize(Roles = "vendor")]
        public IActionResult AddPost(AddPostViewModel Data)
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
        public IActionResult SoftDeletePost(int PostID)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.IsDeleted = true;
            PostManager.Update(Data);
            return Ok("Deleted");
        }

        [Authorize(Roles = "vendor,admin")]
        public IActionResult UpdatePost(int PostID, EditPostViewModel OldPost)
        {
            var Data = PostManager.GetPostByID(PostID);
            Data.Title = OldPost.Title;
            Data.Description = OldPost.Description;
            Data.DateTime = OldPost.DateTime;
            Data.ServiceID = OldPost.ServiceID;

            PostManager.Update(Data);
            return Ok("Updated");
        }
    }
}

