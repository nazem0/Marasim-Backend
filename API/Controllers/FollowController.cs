using Microsoft.AspNetCore.Http;
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
        public FollowController(FollowManager _FollowManager)
        {
            FollowManager = _FollowManager;
        }

        [HttpGet("GetFollowersVendor/{vendorId}")]
        public IActionResult GetFollowersForVendor(int vendorId)
        {
            var users = FollowManager.GetFollowersVendor(vendorId)
                .Include(f => f.User)
                .Select(f => f.ToViewModel(f.User))
                .ToList();
            return new JsonResult(users);
        }

        [HttpGet("Add/{id}")]
        public IActionResult Add(int id)
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
            var res = FollowManager.Add(new Follow() { UserId = UserId, VendorId = id, DateTime = DateTime.Now });
            FollowManager.Save();
            return Ok(res);
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Delete(int id)
        {
            Follow? Follow = FollowManager.GetFollowByID(id);
            if (Follow is not null)
            {
                FollowManager.Delete(Follow);
                FollowManager.Save();
                return Ok("Deleted");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
