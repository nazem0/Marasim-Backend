using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using ViewModels.PostViewModels;
using ViewModels.PromoCodeViewModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly PromoCodeManager PromoCodeManager;

        public PromoCodeController(PromoCodeManager _PromoCodeManger)
        {
            PromoCodeManager = _PromoCodeManger;
        }

        [Authorize(Roles = "vendor")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreatePromoCodeViewModel Data)
        {
            if (!ModelState.IsValid)
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);
            }
            PromoCodeManager.Add(Data.ToModel());
            PromoCodeManager.Save();
            return Ok();
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


        [HttpGet("GetPromoCodes")]
        public IActionResult GetPromoCodes()
        {
            var promoCodes = PromoCodeManager.Get().ToList();
            return Ok(promoCodes);
        }


        [Authorize(Roles = "vendor")]
        [HttpDelete("Delete/{ServiceID}")]
        public IActionResult Delete(int ServiceID)
        {
            var promoCode = PromoCodeManager.GetPromoCodeByServicID(ServiceID);

            if (promoCode != null)
            {
                PromoCodeManager.Delete(promoCode);
                PromoCodeManager.Save();
                return Ok();
            }
            else
            {
                return NotFound("PromoCode not found");
            }
        }


    }
}
