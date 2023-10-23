using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class BookingController : Controller
    {
        private BookingManager BookingManager { get; set; }
        public BookingController(BookingManager _BookingManager)
        {
            BookingManager = _BookingManager;
        }

        public IActionResult Index()
        {
            var x = BookingManager.Get().ToList();
            return Json(x);
        }
    }
}
