using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Models;
namespace Marasim_Backend.Controllers
{
    public class UserController : ControllerBase
    {
        private UserManager<User> UserManager { get; set; }
        public UserController(UserManager<User> _UserManager)
        {
            UserManager = _UserManager;
        }
        public IActionResult Index()
        {
            var x = UserManager.Users;
            return new JsonResult(x);
        }
    }
}
