using Microsoft.AspNetCore.Mvc;
using Repository;


namespace Marasim_Backend.Controllers
{
    public class ServiceController : ControllerBase
    {

        private ServiceManager ServiceManager { get; set; }
        public ServiceController(ServiceManager _ServiceManager)
        {
            ServiceManager = _ServiceManager;
        }
        public IActionResult Index()
        {
            var x = ServiceManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}


