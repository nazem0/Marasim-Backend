using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class UserController : ControllerBase
    {
        private UserManager UserManager { get; set; }
        public UserController(UserManager _UserManager)
        {
            UserManager = _UserManager;
        }
        public IActionResult Index()
        {
            var x = UserManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}
