using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.ReservationViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationManager ReservationManager;
        private readonly VendorManager VendorManager;
        public ReservationController(ReservationManager reservationManager, VendorManager vendorManager)
        {
            ReservationManager = reservationManager;
            VendorManager = vendorManager;
        }
        [HttpPost("Add"), Authorize()]
        public IActionResult Add([FromForm] AddReservationViewModel Data)
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
            EntityEntry<Reservation>? Entry = ReservationManager.Add(Data);
            if (Entry is null)
            {
                return BadRequest("Service Doesn't Exist");
            }
            if (Entry.State == EntityState.Added)
            {
                ReservationManager.Save();
                return Ok();
            }
            else
            {
                return BadRequest(Entry.State);
            }
        }
        [HttpPut("Accept"), Authorize(Roles = "vendor")]
        public IActionResult Accept([FromBody] ChangeReservationStatusViewModel Data)
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
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId; if (Data.VendorId != VendorId)
                return Unauthorized("This Reservation Doesn't Belong To You.");

            EntityEntry<Reservation>? Entry = ReservationManager.ChangeStatus(Data.Id, 'a');
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");

            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok();
            }

        }
        [HttpPut("Reject"), Authorize(Roles = "vendor")]
        public IActionResult Reject([FromBody] ChangeReservationStatusViewModel Data)
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
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            if (Data.VendorId != VendorId)
                return Unauthorized("This Reservation Doesn't Belong To You.");

            EntityEntry<Reservation>? Entry = ReservationManager.ChangeStatus(Data.Id, 'r');
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");

            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok();
            }

        }
        [HttpGet("GetAllUserReservations"), Authorize()]
        public IActionResult GetAllUserReservations()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            return Ok(ReservationManager.GetUserReservations(UserId));
        }

        [HttpGet("GetUserReservationsByStatus/{Status}"), Authorize()]
        public IActionResult GetUserReservationsByStatus(char Status)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(ReservationManager.GetUserReservationsByIdAndStatus(UserId, Status));
        }
        [HttpGet("GetAllVendorReservations"), Authorize()]
        public IActionResult GetAllVendorReservations()
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            return Ok(ReservationManager.GetVendorReservations(VendorId));
        }
        

        [HttpGet("GetVendorReservationsByStatus/{Status}"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorReservationsByStatus(char Status)
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            return Ok(ReservationManager.GetVendorReservationsByIdAndStatus(VendorId, Status));
        }

        [HttpGet("GetVendorReservationsByPagination/{Status}"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorReservationsByPagination(char Status, int PageSize = 8, int PageIndex = 1)
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;
            var paginationResult = ReservationManager.GetVendorReservationsByPagination(VendorId, Status, PageSize, PageIndex);

            return Ok(paginationResult);
        }

        [HttpGet("CheckoutReservationById/{Id}"), Authorize()]
        public IActionResult CheckoutReservationById(int Id)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(ReservationManager.CheckoutReservationById(UserId, Id));
        }
        [HttpGet("Confirm/{Id}"), Authorize()]
        public IActionResult Confirm(int Id)
        {
            EntityEntry<Reservation>? Entry = ReservationManager.Confirm(Id);
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");
            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok();
            }
        }

        [HttpPut("Done"), Authorize()]
        public IActionResult Done([FromBody] UserChangeReservationStatusViewModel Data)
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
            if (Data.UserId != UserId)
                return Unauthorized("This Reservation Doesn't Belong To You.");

            EntityEntry<Reservation>? Entry = ReservationManager.ChangeStatus(Data.Id, 'd');
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");

            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok();
            }

        }


        [HttpGet("GetTotalOrder"), Authorize(Roles = "vendor")]
        public IActionResult GetTotalOrder()
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null) return Unauthorized();
            int VendorId = (int)_vendorId;

            var stats = ReservationManager.GetReservationTotalDoneOrders(VendorId, DateTime.Now.Year);

            return Ok(stats);
        }

        [HttpGet("GetTotalSales"), Authorize(Roles = "vendor")]
        public IActionResult GetTotalSales()
        {
            int? _vendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (_vendorId is null)
                return Unauthorized();
            int VendorId = (int)_vendorId;
            var stats = ReservationManager.GetReservationTotalSales(VendorId, DateTime.Now.Year);

            return Ok(stats);
        }




    }
}
