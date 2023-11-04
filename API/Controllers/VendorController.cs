using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class VendorController : ControllerBase
    {
        private readonly VendorManager VendorManager;
        public VendorController(VendorManager _VendorManager)
        {
            VendorManager = _VendorManager;
        }
        public IActionResult Index()
        {
            var x = VendorManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}

