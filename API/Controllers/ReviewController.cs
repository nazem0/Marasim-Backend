using Application.DTOs.ReviewDTOs;
using Application.ExtensionMethods;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController
            (IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        [HttpGet("GetByServiceId/{ServiceId}")]
        public IActionResult GetByServiceId(int ServiceId)
        {
            var Data = _reviewRepository.GetByServiceId(ServiceId);
            Data.Count();
            return new JsonResult(Data);
        }

        [HttpGet("GetByVendorId/{VendorId}")]
        public IActionResult GetByVendorId(int VendorId)
        {
            var Data = _reviewRepository.GetByVendorId(VendorId)
                .Select(r => r.ToReviewWithServiceDTO());
            return new JsonResult(Data);
        }

        [HttpGet("GetPagedReviewsByVendorId/{VendorId}")]
        public IActionResult GetPagedReviewsByVendorId(int VendorId, int PageIndex = 1, int PageSize = 3)
        {
            var Data = _reviewRepository.GetPaginatedReviewsByVendorId(VendorId, PageIndex, PageSize);
            return Ok(Data);
        }

        [HttpGet("GetAverageRate/{VendorId}")]
        public IActionResult GetAverageRate(int VendorId)
        {
            var Data = _reviewRepository.GetAverageRate(VendorId);
            return Ok(Data);
        }

        [Authorize(Roles = "user")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] CreateReviewDTO Data)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (ModelState.IsValid)
            {
                _reviewRepository.Add(Data, UserId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[Authorize(Roles = "user,admin")]
        //[HttpPut("Update/{ReviewId}")]
        //public IActionResult Update(int ReviewId, [FromForm] UpdateReviewViewModel OldReview)
        //{
        //    string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        //    var Data = ReviewManager.GetReviewById(ReviewId);
        //    if (Data.UserId == UserId)
        //    {
        //        Data.Message = OldReview.Message ?? Data.Message;
        //        Data.Rate = OldReview.Rate;
        //        ReviewManager.Update(Data);
        //        ReviewManager.Save();
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("UserId not matched");
        //    }
        //}

        [Authorize(Roles = "user,admin")]
        [HttpDelete("Delete/{ReviewId}")]
        public IActionResult Delete(int ReviewId)
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = _reviewRepository.Delete(ReviewId, UserId);
            if (result != HttpStatusCode.OK) return BadRequest();
            return Ok();
        }
        [HttpGet("HasReviews/{Id}")]
        public IActionResult HasReviews(int Id)
        {
            return Ok(_reviewRepository.HasReviews(Id));
        }

    }
}

