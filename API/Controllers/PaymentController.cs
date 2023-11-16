using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.PaymentViewModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentManager PaymentManager;
        private readonly ReservationManager ReservationManager;
        private readonly VendorManager VendorManager;
        public PaymentController(
            PaymentManager _paymentManager,
            ReservationManager _reservationManager,
            VendorManager _vendorManager
            )
        {
            PaymentManager = _paymentManager;
            ReservationManager = _reservationManager;
            VendorManager = _vendorManager;
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
            return Ok(PaymentManager.GetPayments());
        }
        [HttpGet("GetUnconfirmed")]
        public IActionResult GetUnconfirmed()
        {
            return Ok(PaymentManager.GetUnconfirmed());
        }
        [HttpGet("GetConfirmed")]
        public IActionResult GetConfirmed()
        {
            return Ok(PaymentManager.GetConfirmed());
        }
        [HttpGet("GetVendorsPayments/{PageIndex}"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorsPayments(int PageIndex = 1,int PageSize =3)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? _vendorId = VendorManager.GetVendorIdByUserId(UserId);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            return Ok(PaymentManager.GetVendorsPayment(VendorId, PageIndex, PageSize));
        }
    }
}
