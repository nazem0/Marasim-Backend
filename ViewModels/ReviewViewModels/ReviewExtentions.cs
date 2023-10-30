using Models;

namespace ViewModels.ReviewViewModels
{
    public static class ReviewExtentions
    {
        public static Review ToModel(this AddReviewViewModel AddReview, string UserID)
        {
            return new Review
            {
                UserID = UserID,
                ServiceID = AddReview.ServiceID,
                Rate = AddReview.Rate,
                Message = AddReview.Message,
                DateTime = AddReview.DateTime
            };
        }

    }
}

