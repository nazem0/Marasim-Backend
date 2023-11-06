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

        [HttpGet("GetByServiceID/{ServiceID}")]
        public IActionResult GetByServiceID(int ServiceID)
        {
            var Data = ReviewManager.GetByServiceID(ServiceID);
            Data.Count();
            return new JsonResult(Data);
        }

        [HttpGet("GetByVendorID/{VendorID}")]
        public IActionResult GetByVendorID(int VendorID)
        {
            var Data = ReviewManager.GetByVendorID(VendorID);
            Data.Count();
            return new JsonResult(Data);
        }

        [Authorize(Roles = "user")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddReviewViewModel Data)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (ModelState.IsValid)
            {
                ReviewManager.Add(Data.ToModel(UserID));
                ReviewManager.Save();
                return Ok("Review Added");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpPut("Update/{ReviewID}")]
        public IActionResult Update(int ReviewId, [FromForm] UpdateReviewViewModel OldReview)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewByID(ReviewId);
            if (Data.UserId == UserId)
            {
                Data.Message = OldReview.Message ?? Data.Message;
                Data.Rate = OldReview.Rate;
                ReviewManager.Update(Data);
                ReviewManager.Save();
                return Ok("Updated");
            }
            else
            {
                return BadRequest("UserID not matched");
            }
        }

        [Authorize(Roles = "user,admin")]
        [HttpDelete("Delete/{ReviewID}")]
        public IActionResult Delete(int ReviewID)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewByID(ReviewID);
            if (Data.UserId == UserId)
            {
                ReviewManager.Delete(Data);
                ReviewManager.Save();
                return Ok("Deleted");
            }
            else
            {
                return BadRequest("UserID not matched");
            }
        }

    }
}

