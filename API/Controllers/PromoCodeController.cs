using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class PromoCodeController : Controller
    {
        private PromoCodeManager PromoCodeManager { get; set; }
        public PromoCodeController(PromoCodeManager _PromoCodeManger)
        {
            PromoCodeManager = _PromoCodeManger;
        }
        public IActionResult Index()
        {

            return Json(PromoCodeManager.Get().ToList());
        }
    }

}
