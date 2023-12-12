using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.InvitationViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly InvitationRepository InvitationManager;
        public InvitationController(InvitationRepository _invitationManager)
        {
            InvitationManager = _invitationManager;
        }

        [HttpPost("Add"), Authorize(Roles = "user")]
        public IActionResult Add([FromForm] AddInvitationViewModel Data)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                bool Result = InvitationManager.Add(Data, UserId);
                if (!Result)
                    return BadRequest();
                else
                    return Ok();
            }
        }

        [HttpGet("Delete/{Id}"), Authorize(Roles = "user")]
        public IActionResult Delete(int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                bool Result = InvitationManager.Delete(Id, UserId);
                if (!Result)
                    return BadRequest();
                else
                    return Ok();
            }
        }

        [HttpGet("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            Invitation? Invitation = InvitationManager.Get(Id);
            if (Invitation is null)
                return NotFound();
            else
                return Ok(Invitation.ToInvitationViewModel());
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            IQueryable<Invitation>? Invitations = InvitationManager.Get().Where(i => i.UserId == UserId)!;
            if (Invitations is null)
                return Ok(0);
            else
                return Ok(Invitations.Select(i => i.ToInvitationViewModel()));
        }
    }
}
