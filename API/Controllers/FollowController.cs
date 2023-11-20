using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.FollowViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly FollowManager FollowManager;
        public FollowController(
            FollowManager _FollowManager)
        {
            FollowManager = _FollowManager;
        }

        [HttpGet("GetFollowersVendor/{VendorId}")]
        public IActionResult GetFollowersForVendor(int VendorId, int PageSize = 2, int PageIndex = 1)
        {
            var Users = FollowManager.GetFollowersVendor(VendorId, PageSize, PageIndex);
            return Ok(Users);
        }

        [HttpGet("GetFollowingForUser")]
        public IActionResult GetFollowingForUser(int PageSize = 2, int PageIndex = 1)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier!)!;
            var Vendors = FollowManager.GetFollowingForUser(UserId, PageSize, PageIndex);
            return Ok(Vendors);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "user")]
        public IActionResult Add([FromForm] AddFollowViewModel Follow)
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
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FollowManager.Add(Follow.ToEntity(UserId!));
            FollowManager.Save();
            return Ok();
        }

        [HttpDelete("Remove/{VendorId}")]
        [Authorize(Roles = "user")]
        public IActionResult Delete(int VendorId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Follow Follow = FollowManager.GetFollow(UserId!, VendorId);
            if (Follow != null)
            {
                FollowManager.Delete(Follow);
                FollowManager.Save();
                return Ok();
            }
            else
            {
                return BadRequest("Not Followed");
            }
        }

        [HttpGet("IsUserFollowingVendor/{VendorId}")]
        public IActionResult IsUserFollowingVendor(int VendorId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId == null) { return BadRequest(); }
            bool Bool = FollowManager.IsUserFollowingVendor(UserId!, VendorId);
            if (Bool)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

    }
}
