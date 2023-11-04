using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using ViewModels.PromoCodeViewModel;

namespace API.Controllers
{
    public class PromoCodeController : ControllerBase
    {
        private readonly PromoCodeManager PromoCodeManager;

        public PromoCodeController(PromoCodeManager _PromoCodeManger)
        {
            PromoCodeManager = _PromoCodeManger;
        }
        public IActionResult Index()
        {

            return new JsonResult(PromoCodeManager.Get().ToList());
        }
        [HttpPost]
        [Authorize(Roles = "vendor")]
        public IActionResult Add([FromBody] CreatePromoCodeViewModel data)
        {
            if (ModelState.IsValid)
            {
                PromoCode newPromoCode = new()
                {
                    ServiceID = data.ServiceID,
                    Code = data.Code,
                    Discount = data.Discount,
                    Limit = data.Limit,
                    Count = data.Count,
                    StartDate = DateTime.Now,
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
        #region Update
        //[Authorize(Roles = "vendor")]
        //public IActionResult Update( [FromForm] UpdatePromoCodeViewModel updatedPromoCode)
        //{
        //    PromoCode? promoCode = PromoCodeManager.Get(updatedPromoCode.Id).FirstOrDefault();

        //    if (promoCode != null)
        //    {
        //        promoCode.Code = updatedPromoCode.Code;
        //        promoCode.Discount = updatedPromoCode.Discount;
        //        promoCode.Limit = updatedPromoCode.Limit;
        //        promoCode.Count = updatedPromoCode.Count;
        //        promoCode.ExpirationDate = updatedPromoCode.ExpirationDate;

        //        PromoCodeManager.Update(promoCode);
        //        PromoCodeManager.Save();

        //        return Ok("PromoCode updated successfully");
        //    }
        //    else
        //    {
        //        return NotFound("PromoCode not found");
        //    }
        //}
        #endregion


        public IActionResult GetPromoCodes()
        {
            var promoCodes = PromoCodeManager.Get().ToList();
            return Ok(promoCodes);
        }

        public IActionResult Delete(int ID)
        {
            PromoCode? promoCode = PromoCodeManager.Get(ID).FirstOrDefault();

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
