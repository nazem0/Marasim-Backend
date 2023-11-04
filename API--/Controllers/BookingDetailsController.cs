using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class BookingDetailsController : ControllerBase
    {
        private BookingManager BookingManager { get; set; }
        public BookingDetailsController(BookingManager _BookingManager)
        {
            BookingManager = _BookingManager;
        }
        public IActionResult Index()
        {

            return new JsonResult(BookingManager.Get().ToList());
        }
    }
}
