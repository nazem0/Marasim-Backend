using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.CommentViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentManager CommentManager;
        public CommentController(CommentManager _CommentManager)
        {
            CommentManager = _CommentManager;
        }

        [HttpGet("GetCommentsByPostID/{PostID}")]
        public IActionResult GetCommentsByPostID(int PostID)
        {
            var Data = CommentManager.GetByPostID(PostID)
                        .Include(c => c.User)
                        .Select(c => c.ToViewModel(c.User));
            return new JsonResult(Data);
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddCommentViewModel Data)
        {
            if (!ModelState.IsValid)
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
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            CommentManager.Add(Data.ToComment(LoggedInUserId));
            CommentManager.Save();
            return Ok("Comment Added");
        }

        [Authorize]
        [HttpDelete("Delete/{CommentID}")]
        public IActionResult Delete(int CommentID)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Comment = CommentManager.Get(CommentID).FirstOrDefault();
            if (Comment is not null && Comment.UserID == UserId)
            {
                CommentManager.Delete(Comment);
                CommentManager.Save();
                return Ok("Comment Deleted");
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

    }
}
