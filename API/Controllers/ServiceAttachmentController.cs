using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class ServiceAttachmentController : ControllerBase
    {
        private ServiceAttachmentManager ServiceAttachmentManager { get; set; }
        public ServiceAttachmentController(ServiceAttachmentManager _ServiceAttachmentManager)
        {
            ServiceAttachmentManager = _ServiceAttachmentManager;
        }
        public IActionResult Index()
        {
            var x = ServiceAttachmentManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}
