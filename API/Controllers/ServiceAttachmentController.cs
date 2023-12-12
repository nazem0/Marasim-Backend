using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;
using ViewModels.ServiceAttachmentViewModels;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAttachmentController : ControllerBase
    {
        private readonly ServiceAttachmentRepository ServiceAttachmentManager;
        private readonly VendorRepository VendorManager;
        public ServiceAttachmentController(ServiceAttachmentRepository _ServiceAttachmentManager, VendorRepository _vendorManager)
        {
            ServiceAttachmentManager = _ServiceAttachmentManager;
            VendorManager = _vendorManager;
        }

        [HttpGet("GetAllActive")]
        public IActionResult GetAllActive()
        {
            var Data = ServiceAttachmentManager.GetAllActive();
            return new JsonResult(Data);
        }

        [HttpGet("GetByServiceId/{ServiceId}")]
        public IActionResult GetByServiceId(int ServiceId)
        {
            return Ok(ServiceAttachmentManager.GetByServiceId(ServiceId));
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId)
        {
            var Data = ServiceAttachmentManager.GetByVendorId(VendorId);
            return new JsonResult(Data);
        }

        [HttpGet("GetAllCustom")]
        public IActionResult GetAllCustom()
        {
            var Data = ServiceAttachmentManager.GetCustomAttachment();
            return new JsonResult(Data);
        }
        [HttpPost("Add"), Authorize(Roles = "vendor")]
        public IActionResult Add([FromForm] AddServiceAttachmentDTO Data)
        {
            int VendorId = (int)VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!)!;
            if (ServiceAttachmentManager.Add(Data,VendorId) is false)
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
            bool result = ServiceAttachmentManager.Delete(Id, VendorId);
            if(result is false) return BadRequest();
            return Ok();
        }
    }
}
