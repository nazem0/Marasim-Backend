using Application.DTOs.FollowDTOs;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _followRepository;
        public FollowController(
            IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        [HttpGet("GetFollowersVendor/{VendorId}")]
        public IActionResult GetFollowersForVendor(int VendorId, int PageIndex = 1, int PageSize = 2)
        {
            var Users = _followRepository.GetFollowersVendor(VendorId, PageIndex, PageSize);
            return Ok(Users);
        }

        [HttpGet("GetFollowingForUser")]
        public IActionResult GetFollowingForUser(int PageIndex = 1, int PageSize = 2)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier!)!;
            var Vendors = _followRepository.GetFollowingForUser(UserId, PageIndex, PageSize);
            return Ok(Vendors);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "user")]
        public IActionResult Add([FromForm] CreateFollowDTO Follow)
        {
            if (!ModelState.IsValid)
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str.ToString());
            }
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            _followRepository.Follow(Follow, UserId);
            return Ok();
        }

        [HttpDelete("Remove/{VendorId}")]
        [Authorize(Roles = "user")]
        public IActionResult Delete(int VendorId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _followRepository.Unfollow(UserId, VendorId);
            if (result is not HttpStatusCode.OK) return BadRequest();
            return Ok();
        }

        [HttpGet("IsUserFollowingVendor/{VendorId}"), Authorize]
        public IActionResult IsUserFollowingVendor(int VendorId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return Ok(_followRepository.CheckUserFollowingVendor(UserId!, VendorId));
        }

    }
}
