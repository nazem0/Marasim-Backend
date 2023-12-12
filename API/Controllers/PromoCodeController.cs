﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text;
using ViewModels.PostViewModels;
using ViewModels.PromoCodeViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly PromoCodeRepository PromoCodeManager;

        public PromoCodeController(PromoCodeRepository _PromoCodeManger)
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


        //[HttpGet("GetPromoCodes")]
        //ViewModelNeeded IN MANAGER
        //public IActionResult GetPromoCodes()
        //{
        //    var PromoCodes = PromoCodeManager.Get().ToList();
        //    return Ok(promoCodes);
        //}


        [Authorize(Roles = "vendor")]
        [HttpDelete("Delete/{ServiceId}")]
        public IActionResult Delete(int ServiceId)
        {
            var promoCode = PromoCodeManager.GetPromoCodeByServiceId(ServiceId);

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
