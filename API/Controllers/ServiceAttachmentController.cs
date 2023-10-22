using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Marasim_Backend.Controllers
{
	public class ServiceAttachmentController : Controller
	{
		private ServiceAttachmentManager ServiceAttachmentManager { get; set; }
		public ServiceAttachmentController(ServiceAttachmentManager _ServiceAttachmentManager)
		{
			ServiceAttachmentManager = _ServiceAttachmentManager;
		}
		public IActionResult Index()
		{
			var x = ServiceAttachmentManager.Get().ToList();
			return Json(x);
		}
	}
}
