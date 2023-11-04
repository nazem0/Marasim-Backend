using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.CommentViewModels;

namespace API.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly CommentManager CommentManager;
        public CommentController(CommentManager commentManager)
        {
            CommentManager = commentManager;
        }
        [Authorize]
        public IActionResult Add(AddCommentViewModel Data)
        {
            string LoggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Comment? CreatedComment = CommentManager.Add(Data.ToComment(LoggedInUserId)).Entity;
            return
                CreatedComment != null ?
                new JsonResult(CreatedComment)
                :
                new JsonResult("Your Comment Wasn't Added, Something Went Wrong");
        }
    }
}
