using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Marasim_Backend.Controllers
{
    public class PromoCodeController : ControllerBase
    {
        private PromoCodeManager PromoCodeManager { get; set; }
        public PromoCodeController(PromoCodeManager _PromoCodeManger)
        {
            PromoCodeManager = _PromoCodeManger;
        }
        public IActionResult Index()
        {

            return new JsonResult(PromoCodeManager.Get().ToList());
        }
    }

}
