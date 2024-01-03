using Application.DTOs.CustomerDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marasim_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public UserController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int PageIndex = 1, int PageSize = 5)
        {
            var Data = await _customerRepository.GetAll(PageIndex, PageSize);
            return Ok(Data);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _customerRepository.Count());
        }

        [HttpGet("UserDetails/{UserId}")]
        public async Task<IActionResult> UserDetails(string UserId)
        {
            var User = await _customerRepository.GetByIdAsync(UserId);
            if (User is null) return NotFound();
            return Ok(User);
        }

        [HttpPut("Update"), Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateCustomerDTO Data)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _customerRepository.UpdateAsync(Data, loggedInUserId);
            if (!result.Succeeded) return BadRequest();
            else if (result != IdentityResult.Success) return BadRequest(result.Errors.ToList());
            else return Ok();
        }
    }
}
