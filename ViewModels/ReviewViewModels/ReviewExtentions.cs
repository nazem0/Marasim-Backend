using Models;

namespace ViewModels.ReviewViewModels
{
    public static class ReviewExtentions
    {
        public static Review ToModel(this AddReviewViewModel AddReview, string UserId)
        {
            return new Review
            {
                UserId = UserId,
                ReservationId = AddReview.ReservationId,
                ServiceId = AddReview.ServiceId,
                Rate = AddReview.Rate,
                Message = AddReview.Message,
                DateTime = AddReview.DateTime
            };
        }

    }
}

