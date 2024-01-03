using Application.DTOs.ReviewDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class ReviewExtensions
    {
        public static Review ToEntity(this CreateReviewDTO createReviewDTO, string loggedInUserId)
        {
            return new Review
            {
                UserId = loggedInUserId,
                ReservationId = createReviewDTO.ReservationId,
                ServiceId = createReviewDTO.ServiceId,
                Rate = createReviewDTO.Rate,
                Message = createReviewDTO.Message,
                DateTime = DateTime.Now
            };
        }

        public static ReviewDTO ToReviewDTO(this Review Data)
        {
            return new ReviewDTO
            {
                DateTime = Data.DateTime,
                Message = Data.Message,
                Rate = Data.Rate,
                User = Data.User.ToCustomerMinInfoDTO()
            };
        }

        public static ReviewWithServiceDTO ToReviewWithServiceDTO(this Review Data)
        {
            return new ReviewWithServiceDTO
            {
                Id = Data.Id,
                DateTime = Data.DateTime,
                Message = Data.Message,
                ServiceId = Data.ServiceId,
                ServiceTitle = Data.Service.Title,
                Rate = Data.Rate,
                User = Data.User.ToCustomerMinInfoDTO()
            };
        }

    }
}

