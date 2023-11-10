using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceAttachmentController : ControllerBase
    {
        private readonly ServiceAttachmentManager ServiceAttachmentManager;
        public ServiceAttachmentController(ServiceAttachmentManager _ServiceAttachmentManager)
        {
            ServiceAttachmentManager = _ServiceAttachmentManager;
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
            var Data = ServiceAttachmentManager.Get().Where(sa => sa.ServiceID == ServiceId);
            return new JsonResult(Data);
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId)
        {
            var Data = ServiceAttachmentManager.GetByVendorId(VendorId);
            return new JsonResult(Data);
        }
    }
}
