using Application.DTOs.ReactDTOs;
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
    public class ReactController : ControllerBase
    {
        private readonly IReactRepository _reactRepository;
        public ReactController
            (IReactRepository __reactRepository)
        {
            _reactRepository = __reactRepository;
        }

        [HttpGet("GetByPostId/{PageIndex}")]
        public IActionResult GetByPostId(int PostId, int PageIndex = 1, int PageSize = 10)
        {
            var Data = _reactRepository.GetByPostId(PostId, PageIndex, PageSize);

            return new JsonResult(Data);
        }
        [HttpGet("GetReactsCountByPostId/{PostId}")]
        public IActionResult GetReactsCountByPostId(int PostId)
        {
            return Ok(_reactRepository.GetReactsCountByPostId(PostId));
        }

        [HttpGet("GetIsLiked/{PostId}"), Authorize]
        public IActionResult GetIsLiked(int PostId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (_reactRepository.IsLiked(UserId, PostId))
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
        public IActionResult Add([FromForm] CreateReactDTO createReactDTO)
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
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _reactRepository.Add(createReactDTO, UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

        [Authorize]
        [HttpDelete("Delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _reactRepository.DeleteByPostId(PostId, UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
    }
}
