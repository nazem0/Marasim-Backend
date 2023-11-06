using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using ViewModels.ReactViewModel;
using ViewModels.UserViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly ReactManager ReactManager;
        public ReactController
            (ReactManager _ReactManager)
        {
            ReactManager = _ReactManager;
        }

        [HttpGet("GetReactsByPostID/{PostID}")]
        public IActionResult GetReactsByPostID(int PostID)
        {
            var Data = ReactManager.GetByPostID(PostID)
                         .Include(r => r.User)
                         .Select(r => r.ToViewModel(r.User));

            return new JsonResult(Data);
        }

        [HttpGet("GetIsLiked/{PostID}")]
        public IActionResult GetIsLiked(int PostId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ReactManager.GetByPostID(PostId).Any(r => r.UserId == UserId))
            {
                return new JsonResult("true");
            }
            else
            {
                return new JsonResult("false");
            }
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddReactViewModel AddReact)
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
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ReactManager.IsLiked(UserId!))
            {
                ReactManager.Add(AddReact.ToModel(UserId!));
                ReactManager.Save();
                return Ok("Liked");
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

        [Authorize]
        [HttpDelete("Delete/{PostID}")]
        public IActionResult Delete(int PostID)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var React = ReactManager.GetByPostID(PostID).FirstOrDefault();
            if (React is not null && React.UserId == UserId)
            {
                ReactManager.Delete(React);
                ReactManager.Save();
                return Ok("Disliked");
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
