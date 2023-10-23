using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class ReveiwController : ControllerBase
    {
        private ReveiwManager ReveiwManager { get; set; }
        public ReveiwController(ReveiwManager _ReveiwManager)
        {
            ReveiwManager = _ReveiwManager;
        }
        public IActionResult Index()
        {
            var x = ReveiwManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}
