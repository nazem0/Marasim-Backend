using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class BookingDetailsController : Controller
    {
        private BookingManager BookingManager { get; set; }
        public BookingDetailsController(BookingManager _BookingManager)
        {
            BookingManager = _BookingManager;
        }
        public IActionResult Index()
        {

            return Json(BookingManager.Get().ToList());
        }
    }
}
