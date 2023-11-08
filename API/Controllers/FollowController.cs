using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.FollowViewModels;

namespace Api.Controllers
{
   
    public class FollowController : ControllerBase
    {
        private readonly FollowManager FollowManager;
        public FollowController(FollowManager _FollowManager)
        {
            FollowManager = _FollowManager;
        }
        public IActionResult Index()
        {

            var x = FollowManager.Get().ToList();
            return new JsonResult(x);
        }
        

        [HttpGet("AddFollow/{id}")]
        public IActionResult AddFollow( int id)
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
            FollowManager.Add(new Follow() {UserId=UserId,VendorId=id, DateTime=DateTime.Now });
            FollowManager.Save();
            return Ok("Follow added successfully.");
        }



        [HttpDelete("removefollow/{id}")]
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
