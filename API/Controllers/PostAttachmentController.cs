using Application.DTOs.PostAttachmentDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAttachmentController : ControllerBase
    {
        private readonly IPostAttachmentRepository _postAttachmentRepository;
        private readonly IVendorRepository _vendorRepository;
        public PostAttachmentController(IPostAttachmentRepository postAttachmentRepository, IVendorRepository vendorRepository)
        {
            _postAttachmentRepository = postAttachmentRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpGet("GetByPostId/{PostId}")]
        public IActionResult GetByPostId(int PostId)
        {
            return Ok(_postAttachmentRepository.GetByPostId(PostId));
        }

        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] CreatePostAttachmentDTO Data)
        {
            int VendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return _postAttachmentRepository.Add(Data, VendorId) is HttpStatusCode.OK ? Ok() : BadRequest();
        }
        [HttpDelete("Delete/{Id}"), Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = _postAttachmentRepository.Delete(Id, vendorId);
            return result is HttpStatusCode.OK ? Ok() : BadRequest();
        }
    }
}
