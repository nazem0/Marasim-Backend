using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using System.Security.Claims;
using System.Text.Json.Nodes;
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
                return Ok(Entry.Entity);
            }
            else
            {
                return BadRequest(Entry.State);
            }
        }
        [HttpPut("Accept"), Authorize(Roles = "vendor")]
        public IActionResult Accept([FromForm] ChangeReservationStatusViewModel Data)
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
            int VendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (Data.VendorId != VendorId)
                return Unauthorized("This Reservation Doesn't Belong To You.");

            EntityEntry<Reservation>? Entry = ReservationManager.ChangeStatus(Data.Id, 'a');
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");

            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok("Service Accepted");
            }

        }
        [HttpPut("Reject"), Authorize(Roles = "vendor")]
        public IActionResult Reject([FromForm] ChangeReservationStatusViewModel Data)
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
            int VendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (Data.VendorId != VendorId)
                return Unauthorized("This Reservation Doesn't Belong To You.");

            EntityEntry<Reservation>? Entry = ReservationManager.ChangeStatus(Data.Id, 'f');
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");

            if (Entry.State != EntityState.Modified)
                return BadRequest(Entry.State);
            else
            {
                ReservationManager.Save();
                return Ok("Service Rejected");
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
            int VendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return Ok(ReservationManager.GetVendorReservations(VendorId));
        }
        [HttpGet("GetVendorReservationsByStatus/{Status}"), Authorize(Roles = "vendor")]
        public IActionResult GetVendorReservationsByStatus(char Status)
        {
            int VendorId = VendorManager.GetVendorIdByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Ok(ReservationManager.GetVendorReservationsByIdAndStatus(VendorId, Status));
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

    }
}
