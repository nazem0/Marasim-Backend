using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Security.Claims;
using ViewModels.PostViewModels;
using ViewModels.ReviewViewModels;

namespace API.Controllers
{
    public class ReviewController : ControllerBase
    {
        private ReviewManager ReviewManager { get; set; }
        public ReviewController
            (ReviewManager _ReviewManager)
        {
            ReviewManager = _ReviewManager;
        }

        public IActionResult Get()
        {
            var Data = ReviewManager.Get().ToList();
            return new JsonResult(Data);
        }

        public IActionResult GetByServiceID(int ServiceID)
        {
            var Data = ReviewManager.GetByServiceID(ServiceID);
            Data.Count();
            return new JsonResult(Data);
        }

        public IActionResult GetByVendorID(int VendorID)
        {
            var Data = ReviewManager.GetByVendorID(VendorID);
            Data.Count();
            return new JsonResult(Data);
        }

        [Authorize(Roles = "user")]
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
        public IActionResult Update(int ReviewID, [FromForm] UpdateReviewViewModel OldReview)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewByID(ReviewID);
            if (Data.UserID == UserID)
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
        public IActionResult Delete(int ReviewID)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var Data = ReviewManager.GetReviewByID(ReviewID);
            if (Data.UserID == UserID)
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

