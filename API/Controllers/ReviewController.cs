using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
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

        [Authorize(Roles = "User")]
        public IActionResult AddReview([FromForm] AddReviewViewModel Data)
        {
            if (ModelState.IsValid)
            {
                string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                ReviewManager.Add(Data.ToModel(UserID));
                ReviewManager.Save();
                return Ok("Review Added");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "User,admin")]
        public IActionResult UpdateReview(int ReviewID, [FromForm] UpdateReviewViewModel OldReview)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (OldReview.UserID == UserID)
            {
                var Data = ReviewManager.GetReviewByID(ReviewID);
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

        [Authorize(Roles = "User,admin")]
        public IActionResult DeleteReview(int ReviewID)
        {
            var Data = ReviewManager.GetReviewByID(ReviewID);
            ReviewManager.Delete(Data);
            ReviewManager.Save();
            return Ok("Deleted");
        }

    }
}

