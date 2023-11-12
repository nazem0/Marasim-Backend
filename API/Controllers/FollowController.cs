using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult GetFollowersForVendor(int VendorId)
        {
            var Users = FollowManager.GetFollowersVendor(VendorId)
                .Include(f => f.User)
                .Select(f => f.ToFollowerViewModel())
                .ToList();
            return new JsonResult(Users);
        }

        [HttpGet("GetFollowingForUser/{UserId}")]
        public IActionResult GetFollowingForUser(string UserId)
        {
            var Vendors = FollowManager.GetFollowingForUser(UserId)
                .Include(f => f.Vendor.User)
                .Select(f => f.ToFollowingViewModel())
                .ToList();
            return new JsonResult(Vendors);
        }

        [HttpPost("Add")]
        [Authorize(Roles = "user")]
        public IActionResult Add([FromBody] AddFollowViewModel Follow)
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
            var res = FollowManager.Add(Follow.ToEntity(UserId!));
            FollowManager.Save();
            return Ok(res);
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
    }
}
