using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;
using ViewModels.PostViewModels;
using ViewModels.ReviewViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewManager ReviewManager;
        public ReviewController
            (ReviewManager _ReviewManager)
        {
            ReviewManager = _ReviewManager;
        }

        [HttpGet("Get")]
        public IActionResult Get()
        {
            var Data = ReviewManager.Get().ToList();
            return new JsonResult(Data);
        }

        [HttpGet("GetByServiceId/{ServiceId}")]
        public IActionResult GetByServiceId(int ServiceId)
        {
            var Data = ReviewManager.GetByServiceId(ServiceId);
            Data.Count();
            return new JsonResult(Data);
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId)
        {
            var Data = ReviewManager.GetByVendorId(VendorId);
            Data.Count();
            return new JsonResult(Data);
        }

        [Authorize(Roles = "user")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddReviewViewModel Data)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (ModelState.IsValid)
            {
                ReviewManager.Add(Data.ToModel(UserId));
                ReviewManager.Save();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpPut("Update/{ReviewId}")]
        public IActionResult Update(int ReviewId, [FromForm] UpdateReviewViewModel OldReview)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewById(ReviewId);
            if (Data.UserId == UserId)
            {
                Data.Message = OldReview.Message ?? Data.Message;
                Data.Rate = OldReview.Rate;
                ReviewManager.Update(Data);
                ReviewManager.Save();
                return Ok();
            }
            else
            {
                return BadRequest("UserId not matched");
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpDelete("Delete/{ReviewId}")]
        public IActionResult Delete(int ReviewId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewById(ReviewId);
            if (Data.UserId == UserId)
            {
                ReviewManager.Delete(Data);
                ReviewManager.Save();
                return Ok();
            }
            else
            {
                return BadRequest("UserId not matched");
            }
        }

    }
}

