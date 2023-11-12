using Models;
using ViewModels.UserViewModels;

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

        public static ReviewViewModel? ToReviewViewModel(this Review Data)
        {
            if (Data is null)
                return null;
            return new ReviewViewModel
            {
                DateTime = Data.DateTime,
                Message = Data.Message,
                Rate = Data.Rate,
                User = Data.User.ToUserMinInfoViewModel()
            };
        }

        public static ReviewFullViewModel ToReviewFullViewModel(this Review Data)
        {
            return new ReviewFullViewModel
            {
                Id = Data.Id,
                DateTime = Data.DateTime,
                Message = Data.Message,
                ServiceId = Data.ServiceId,
                ServiceTitle = Data.Service.Title,
                Rate = Data.Rate,
                User = Data.User.ToUserMinInfoViewModel()
            };
        }

    }
}

