using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;
using ViewModels.PostAttachmentsViewModel;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAttachmentController : ControllerBase
    {
        private readonly PostAttachmentManager PostAttachmentManager;
        private readonly VendorManager VendorManager;
        public PostAttachmentController(PostAttachmentManager _PostAttachmentManager, VendorManager _vendorManager)
        {
            PostAttachmentManager = _PostAttachmentManager;
            VendorManager = _vendorManager;
        }

        [HttpGet("GetByPostId/{PostId}")]
        public IActionResult GetByPostId(int PostId)
        {
            return Ok(PostAttachmentManager.GetByPostId(PostId));
        }

        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] AddPostAttachmentsDTO Data)
        {
            int VendorId = (int)VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!)!;
            if (PostAttachmentManager.Add(Data, VendorId) is false)
                return BadRequest();
            else
                return Ok();
        }
        [HttpDelete("Delete/{Id}"), Authorize(Roles = "vendor")]
        public IActionResult Delete(int Id)
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            bool result = PostAttachmentManager.Delete(Id, VendorId);
            if (result is false) return BadRequest();
            return Ok();
        }
    }
}
