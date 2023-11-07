using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Repository;
using ViewModels.ReservationViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationManager ReservationManager;
        public ReservationController(ReservationManager reservationManager)
        {
            ReservationManager = reservationManager;
        }
        [HttpPost("Add"),Authorize()]
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
            if(Entry is null)
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
    }
}
