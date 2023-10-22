using Microsoft.AspNetCore.Mvc;
using Repository;


namespace Marasim_Backend.Controllers
{
    public class ServiceAttachmentController : Controller
    {
        private ServiceAttachmentManager ServiceAttachmentManager { get; set; }
        public ServiceAttachmentController (ServiceAttachmentManager _ServiceAttachment)
        {
            ServiceAttachmentManager = _ServiceAttachment;
        }
        public IActionResult Index()
        {
            var x = ServiceAttachmentManager.Get().ToList();
            return Json(x);
        }
    }
}