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
        [HttpPut("Accept"), Authorize(Roles ="vendor")]
        public IActionResult Accept([FromForm] AcceptReservation Data)
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
            if (Data.VendorId!=VendorId)
                return Unauthorized("This Reservation Doesn't Belong To You.");
            
            EntityEntry<Reservation>? Entry = ReservationManager.Accept(Data);
            if (Entry is null)
                return BadRequest("Reservation Doesn't Exist");
            
            if (Entry.State == EntityState.Modified)
            {
                ReservationManager.Save();
                return Ok(Entry.Entity);
            }
            else            
                return BadRequest(Entry.State);
            
        }
        [HttpGet("GetAllByUserId/{UserId}")]
        public IActionResult GetAllByUserId(string UserId)
        {
            return Ok(ReservationManager.Get(UserId));
        }
    }
}
