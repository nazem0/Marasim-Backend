using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReviewDTOs;
using Domain.Entities;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IReviewRepository
    {
        public double GetAverageRate(int VendorId);
        public HttpStatusCode Add(CreateReviewDTO createReviewDTO, string loggedInUserId);
        public IQueryable<Review> GetByServiceId(int ServiceId);
        public IQueryable<Review> GetByVendorId(int VendorId);
        public Review GetReviewById(int Id);
        public bool HasReviews(int ReservationId);
        public PaginationDTO<ReviewWithServiceDTO> GetPaginatedReviewsByVendorId(int VendorId, int PageIndex, int PageSize);
        public HttpStatusCode Delete(int reviewId, string userId);
    }
}

