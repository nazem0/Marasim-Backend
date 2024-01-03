using Application.DTOs.InvitationDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationRepository _invitationRepository;
        public InvitationController(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        [HttpPost("Add"), Authorize(Roles = "user")]
        public IActionResult Add([FromForm] CreateInvitationDTO Data)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            HttpStatusCode result = _invitationRepository.Add(Data, UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();

        }

        [HttpGet("Delete/{Id}"), Authorize(Roles = "user")]
        public IActionResult Delete(int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                HttpStatusCode result = _invitationRepository.Delete(Id, UserId);
                if (result != HttpStatusCode.OK) return BadRequest();
                return Ok();
            }
        }

        [HttpGet("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var Invitation = _invitationRepository.GetById(Id);
            if (Invitation is null)
                return NotFound();
            else
                return Ok(Invitation);
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var invitations = _invitationRepository.GetByUserId(UserId);
            return Ok(invitations);
        }
    }
}
