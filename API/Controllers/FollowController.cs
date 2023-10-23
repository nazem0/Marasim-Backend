using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{

    public class FollowController : Controller
    {
        private FollowManager FollowManager { get; set; }
        public FollowController(FollowManager _FollowManger)
        {
            FollowManager = _FollowManger;
        }
        public IActionResult Index()
        {

            var x = FollowManager.Get().ToList();
            return Json(x);
        }
    }

}
