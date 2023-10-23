using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class UserController : Controller
    {
        private UserManager UserManager { get; set; }
        public UserController(UserManager _UserManager)
        {
            UserManager = _UserManager;
        }
        public IActionResult Index()
        {
            var x = UserManager.Get().ToList();
            return Json(x);
        }
    }
}
