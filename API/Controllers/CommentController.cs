using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.CommentViewModels;
using ViewModels.PaginationViewModels;

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

        [HttpGet("GetCommentsByPostId/{PageIndex}")]
        public IActionResult GetCommentsByPostId(int PostId, int PageIndex = 1, int PageSize = 5)
        {
            PaginationViewModel<CommentViewModel> Data = CommentManager.GetByPostId(PostId,PageIndex,PageSize);
            return Ok(Data);
        }
        [HttpGet("GetCommentsCountByPostId/{PostId}")]
        public IActionResult GetCommentsCount(int PostId)
        {
            return Ok(CommentManager.GetCommentsCount(PostId));
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
            return Ok();
        }

        [Authorize]
        [HttpDelete("Delete/{CommentId}")]
        public IActionResult Delete(int CommentId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Comment = CommentManager.Get(CommentId);
            if (Comment is not null && Comment.UserId == UserId)
            {
                CommentManager.Delete(Comment);
                CommentManager.Save();
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

    }
}
