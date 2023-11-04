using Microsoft.AspNetCore.Mvc;
using Repository;


namespace Marasim_Backend.Controllers
{
    public class CheckListController : ControllerBase
    {
        private readonly CheckListManager CheckListManager;
        public CheckListController(CheckListManager _CheckListManager)
        {
            CheckListManager = _CheckListManager;
        }

        public IActionResult Index()
        {
            var x = CheckListManager.Get().ToList();
            return new JsonResult(x);
        }
    }
}
