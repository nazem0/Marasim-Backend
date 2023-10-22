using Microsoft.AspNetCore.Mvc;
using Repository;


namespace Marasim_Backend.Controllers
{
    public class ServiceController : Controller
    {

        private ServiceManager ServiceManager { get; set; }
        public ServiceController(ServiceManager _ServiceManager)
        {
            ServiceManager = _ServiceManager;
        }
        public IActionResult Index()
        {
            var x = ServiceManager.Get().ToList();
            return Json(x);
        }
    }
}


