using Application.DTOs.PromoCodeDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCodeRepository;

        public PromoCodeController(IPromoCodeRepository promoCodeRepository)
        {
            _promoCodeRepository = promoCodeRepository;
        }

        [Authorize(Roles = "vendor")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreatePromoCodeDTO Data)
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
            var result = _promoCodeRepository.Add(Data);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "vendor")]
        [HttpDelete("Delete/{ServiceId}")]
        public IActionResult Delete(int ServiceId)
        {
            var result = _promoCodeRepository.DeleteByServiceId(ServiceId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }


    }
}
