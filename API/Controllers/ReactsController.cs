using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.ReactViewModels;

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

        [HttpGet("GetByPostId/{PageIndex}")]
        public IActionResult GetByPostId(int PostId,int PageIndex=1,int PageSize=10)
        {
            var Data = ReactManager.GetByPostId(PostId,PageIndex,PageSize);

            return new JsonResult(Data);
        }
        [HttpGet("GetReactsCountByPostId/{PostId}")]
        public IActionResult GetReactsCountByPostId(int PostId)
        {
            return Ok(ReactManager.GetReactsCountByPostId(PostId));
        }

        [HttpGet("GetIsLiked/{PostId}"),Authorize]
        public IActionResult GetIsLiked(int PostId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (ReactManager.IsLiked(UserId,PostId))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
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

            if (!ReactManager.IsLiked(UserId!, AddReact.PostId))
            {
                ReactManager.Add(AddReact.ToModel(UserId!));
                ReactManager.Save();
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

        [Authorize]
        [HttpDelete("Delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            React? React = ReactManager.Get().Where(r => r.PostId== PostId && r.UserId == UserId).FirstOrDefault();
            if (React is not null && React.UserId == UserId)
            {
                ReactManager.Delete(React);
                ReactManager.Save();
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
