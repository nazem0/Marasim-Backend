using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class VendorController : Controller
    {
        private VendorManager VendorManager { get; set; }
        public VendorController(VendorManager _VendorManager)
        {
            VendorManager = VendorManager;
        }
        public IActionResult Index()
        {
            var x = VendorManager.Get().ToList();
            return Json(x);
        }
    }
}

