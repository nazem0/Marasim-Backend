using Application.DTOs.CommentDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("GetCommentsByPostId/{PageIndex}")]
        public IActionResult GetCommentsByPostId(int PostId, int PageIndex = 1, int PageSize = 5)
        {
            var Data = _commentRepository.Get(PostId, PageIndex, PageSize);
            return Ok(Data);
        }
        [HttpGet("GetCommentsCountByPostId/{PostId}")]
        public IActionResult GetCommentsCount(int PostId)
        {
            return Ok(_commentRepository.GetCommentsCount(PostId));
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreateCommentDTO Data)
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
            string loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            HttpStatusCode result = _commentRepository.Add(Data, loggedInUser);
            if (result == HttpStatusCode.Conflict) return Conflict();
            if (result != HttpStatusCode.OK) return StatusCode((int)result, "Internal Server Error");
            return Ok();
        }

        [Authorize]
        [HttpDelete("Delete/{CommentId}")]
        public IActionResult Delete(int CommentId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _commentRepository.Delete(CommentId, UserId);
            if (result == HttpStatusCode.NotFound) return NotFound();
            if (result == HttpStatusCode.Forbidden) return Forbid();
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

    }
}
