using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{

    public class FollowController : ControllerBase
    {
        private readonly FollowManager FollowManager;
        public FollowController(FollowManager _FollowManger)
        {
            FollowManager = _FollowManger;
        }
        public IActionResult Index()
        {

            var x = FollowManager.Get().ToList();
            return new JsonResult(x);
        }
    }

}
