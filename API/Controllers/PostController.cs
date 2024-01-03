using Application.DTOs.PostDTOs;
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
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IVendorRepository _vendorRepository;
        public PostController
            (IPostRepository postRepository,
            IVendorRepository vendorRepository)
        {
            _postRepository = postRepository;
            _vendorRepository = vendorRepository;
        }

        //[HttpGet("Get")]
        //public IActionResult Get()
        //{
        //    var Data = _postRepository.Get()
        //        .Select(p => p.ToViewModel());
        //    return Ok(Data);
        //}

        [HttpGet("GetById/{PostId}")]
        public IActionResult GetById(int PostId)
        {
            PostDTO? Post = _postRepository.GetById(PostId);
            if (Post is null)
                return NotFound();
            return Ok(Post);
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId, int PageIndex = 1, int PageSize = 2)
        {
            var Data = _postRepository.GetByVendorId(VendorId, PageIndex, PageSize);
            return Ok(Data);
        }

        [HttpGet("GetByPostsByFollow")]
        public IActionResult GetByPostsByFollow(int PageIndex = 1, int PageSize = 2)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier!)!;
            var Data = _postRepository.GetByPostsByFollow(UserId, PageIndex, PageSize);
            return Ok(Data);
        }

        [Authorize(Roles = "vendor")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreatePostDTO Data)
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


            var result = _postRepository.Add(Data, User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return result is HttpStatusCode.OK ? Ok() : BadRequest();

        }

        [Authorize(Roles = "vendor,admin")]
        [HttpDelete("Delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId
                (User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = _postRepository.Delete(PostId, vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();


        }

        [Authorize(Roles = "vendor")]
        [HttpPut("Update/{PostId}")]
        public IActionResult Update([FromForm] UpdatePostDTO OldPost)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _postRepository.Update(OldPost, loggedInUserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
    }
}

