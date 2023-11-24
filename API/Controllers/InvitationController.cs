using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using ViewModels.InvitationViewModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly InvitationManager InvitationManager;
        public InvitationController(InvitationManager _invitationManager)
        {
            InvitationManager = _invitationManager;
        }
        [HttpPost("Add"), Authorize()]
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
            Invitation? Invitation = InvitationManager.Get().Where(i => i.UserId == UserId).FirstOrDefault()!;
            if (Invitation is null)
                return Ok(0);
            else
                return Ok(Invitation.Id);
        }
    }
}
