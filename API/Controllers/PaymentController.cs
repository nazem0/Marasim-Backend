using Application.DTOs.PaymentDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IVendorRepository _vendorRepository;
        public PaymentController(
            IPaymentRepository paymentRepository,
            IReservationRepository reservationRepository,
            IVendorRepository vendorRepository
            )
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _vendorRepository = vendorRepository;
        }



        [HttpPost("Add"), Authorize()]
        public IActionResult Add([FromForm] CreatePaymentDTO Data)
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
                HttpStatusCode result = _paymentRepository.Add(Data);
                if (result != HttpStatusCode.OK) return BadRequest();
                return Ok();
            }
        }

        [HttpGet("Count")]
        public IActionResult Count()
        {
            return Ok(_paymentRepository.Count());
        }

        //To Be Only For Admin Later
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok(_paymentRepository.GetPayments());
        }
        [HttpGet("GetUnconfirmed")]
        public IActionResult GetUnconfirmed()
        {
            return Ok(_paymentRepository.GetUnconfirmed());
        }
        [HttpGet("GetConfirmed")]
        public IActionResult GetConfirmed()
        {
            return Ok(_paymentRepository.GetConfirmed());
        }
        [HttpGet("GetVendorsPayments/{PageIndex}"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorsPayments(int PageIndex = 1, int PageSize = 3)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            int? _vendorId = _vendorRepository.GetVendorIdByUserId(UserId);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            return Ok(_paymentRepository.GetVendorsPayment(VendorId, PageIndex, PageSize));
        }

        //stats for TotalMonthlyProfits
        [HttpGet("GetOurTotalProfits/{Year?}"), Authorize(Roles = "admin")]
        public IActionResult GetOurTotalProfits(int Year)
        {
            IDictionary<string, double> monthlyTotals = _paymentRepository.GetMonthlyPaymentTotal(Year);
            return Ok(monthlyTotals);
        }


        [HttpGet("GetVendorBalance"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorBalance()
        {
            int? _vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            return Ok(_paymentRepository.VendorBalance(VendorId));
        }
    }
}
