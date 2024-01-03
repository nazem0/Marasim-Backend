using Application.DTOs.ReservationDTOs;
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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IVendorRepository _vendorRepository;
        public ReservationController(IReservationRepository reservationManager, IVendorRepository vendorManager)
        {
            _reservationRepository = reservationManager;
            _vendorRepository = vendorManager;
        }
        [HttpPost("Add"), Authorize()]
        public IActionResult Add([FromForm] CreateReservationDTO Data)
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
            var result = _reservationRepository.Add(Data);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
        [HttpPut("Accept"), Authorize(Roles = "vendor")]
        public IActionResult Accept([FromBody] ChangeReservationStatusDTO Data)
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
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = _reservationRepository.ChangeStatusByVendor(Data.Id, 'a', vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }
        [HttpPut("Reject"), Authorize(Roles = "vendor")]
        public IActionResult Reject([FromBody] ChangeReservationStatusDTO Data)
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
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = _reservationRepository.ChangeStatusByVendor(Data.Id, 'r', vendorId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }
        [HttpGet("Confirm/{Id}"), Authorize()]
        public IActionResult Confirm(int Id)
        {
            var result = _reservationRepository.Confirm(Id);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
        [HttpPut("Done"), Authorize()]
        public IActionResult Done([FromBody] CustomerChangeReservationStatusDTO Data)
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
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = _reservationRepository.ChangeStatusByCustomer(Data.Id, 'd', UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }

        // User Reservations
        [HttpGet("GetAllUserReservations"), Authorize()]
        public IActionResult GetAllUserReservations(int PageIndex = 1, int PageSize = 5)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = _reservationRepository.GetUserReservations(UserId, PageIndex, PageSize);
            return Ok(Data);
        }
        [HttpGet("GetUserReservationsByStatus"), Authorize()]
        public IActionResult GetUserReservationsByStatus(char Status, int PageIndex = 1, int PageSize = 5)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = _reservationRepository.GetUserReservationsByIdAndStatus(UserId, Status, PageIndex, PageSize);
            return Ok(Data);
        }
        [HttpGet("GetAllVendorReservations"), Authorize()]
        public IActionResult GetAllVendorReservations(int PageIndex = 1, int PageSize = 5)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var Data = _reservationRepository.GetVendorReservations(vendorId, PageIndex, PageSize);
            return Ok(Data);
        }

        // Vendor Reservations
        [HttpGet("GetVendorReservationsByStatus"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorReservationsByStatus(char Status, int PageIndex = 1, int PageSize = 5)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var Data = _reservationRepository.GetVendorReservationsByIdAndStatus(vendorId, Status, PageIndex, PageSize);
            return Ok(Data);
        }
        //[HttpGet("GetVendorReservationsByPagination/{Status}"), Authorize(Roles = "vendor")]
        //public IActionResult GetVendorReservationsByPagination(char Status, int PageSize = 8, int PageIndex = 1)
        //{
        //    int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //    if (_vendorId is null) return Unauthorized();
        //    int VendorId = (int)_vendorId;
        //    var paginationResult = ReservationManager.GetVendorReservationsByPagination(VendorId, Status, PageSize, PageIndex);

        //    return Ok(paginationResult);
        //}

        //Checkout
        [HttpGet("CheckoutReservationById/{Id}"), Authorize()]
        public IActionResult CheckoutReservationById(int Id)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(_reservationRepository.CheckoutReservationById(UserId, Id));
        }

        // Stats
        [HttpGet("GetTotalOrder"), Authorize(Roles = "vendor")]
        public IActionResult GetTotalOrder()
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


            var stats = _reservationRepository.GetReservationTotalDoneOrders(vendorId, DateTime.Now.Year);

            return Ok(stats);
        }
        [HttpGet("GetTotalSales"), Authorize(Roles = "vendor")]
        public IActionResult GetTotalSales()
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var stats = _reservationRepository.GetReservationTotalSales(vendorId, DateTime.Now.Year);

            return Ok(stats);
        }
    }
}
