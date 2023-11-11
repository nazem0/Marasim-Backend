using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using System.Text.Json.Nodes;
using ViewModels.PaymentViewModel;
using ViewModels.ReservationViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentManager PaymentManager;
        private readonly ReservationManager ReservationManager;
        public PaymentController(
            PaymentManager _PaymentManager,
            ReservationManager _ReservationManager)
        {
            PaymentManager = _PaymentManager;
            ReservationManager = _ReservationManager;
        }

        [HttpPost("Add"), Authorize()]
        public IActionResult Add([FromForm] AddPaymentViewModel Data)
        {
            if (!ModelState.IsValid)
            {
                List<ModelError> Errors = new();
                foreach (var item in ModelState.Values)
                {
                    foreach (ModelError item1 in item.Errors)
                    {
                        Errors.Add(item1);
                    }
                }
                return BadRequest(Errors);
            }
            else
            {
                EntityEntry<Payment> Entry = PaymentManager.Add(Data);
                if (Entry.State != EntityState.Added)
                    return BadRequest(Entry.State);
                else
                {
                    ReservationManager.ChangeStatus(Data.ReservationId, 'f');

                    PaymentManager.Save();
                    ReservationManager.Save();
                    return Ok();
                }
            }
        }
        //To Be Only For Admin Later
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return new JsonResult(PaymentManager.Get());
        }
    }
}
