using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using ViewModels.PromoCodeViewModel;

namespace API.Controllers
{
    public class PromoCodeController : ControllerBase
    {
        private PromoCodeManager PromoCodeManager { get; set; }
        private ServiceManager ServiceManager { get; set; }

        public PromoCodeController(PromoCodeManager _PromoCodeManger, ServiceManager _ServiceManager)
        {
            PromoCodeManager = _PromoCodeManger;
            ServiceManager = _ServiceManager;
        }
        public IActionResult Index()
        {

            return new JsonResult(PromoCodeManager.Get().ToList());
        }
        [HttpPost]
        [Authorize(Roles = "vendor")]
        public IActionResult AddPromoCode([FromForm] CreatePromoCodeViewModel data)
        {
            if (ModelState.IsValid)
            {
                PromoCode newPromoCode = new PromoCode
                {
                    ServiceID = data.ServiceID,
                    Code = data.Code,
                    Discount = data.Discount,
                    Limit = data.Limit,
                    Count = data.Count,
                    StartDate = data.StartDate,
                    ExpirationDate = data.ExpirationDate
                };

                PromoCodeManager.Add(newPromoCode);
                PromoCodeManager.Save();

                return Ok("PromoCode added successfully");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Authorize(Roles = "vendor")]
        public IActionResult UpdatePromoCode( [FromForm] UpdatePromoCodeViewModel updatedPromoCode)
        {
            PromoCode promoCode = PromoCodeManager.Get(updatedPromoCode.Id);

            if (promoCode != null)
            {
                promoCode.Code = updatedPromoCode.Code;
                promoCode.Discount = updatedPromoCode.Discount;
                promoCode.Limit = updatedPromoCode.Limit;
                promoCode.Count = updatedPromoCode.Count;
                promoCode.ExpirationDate = updatedPromoCode.ExpirationDate;

                PromoCodeManager.Update(promoCode);
                PromoCodeManager.Save();

                return Ok("PromoCode updated successfully");
            }
            else
            {
                return NotFound("PromoCode not found");
            }
        }

        public IActionResult GetPromoCodes()
        {
            var promoCodes = PromoCodeManager.Get().ToList();
            return Ok(promoCodes);
        }

        public IActionResult DeletePromoCode(int ID)
        {
            PromoCode promoCode = PromoCodeManager.Get(ID);

            if (promoCode != null)
            {
                PromoCodeManager.Delete(promoCode);
                PromoCodeManager.Save();
                return Ok("PromoCode deleted successfully");
            }
            else
            {
                return NotFound("PromoCode not found");
            }
        }


    }
}
