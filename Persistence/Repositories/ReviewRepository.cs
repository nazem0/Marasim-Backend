using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReviewDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbSet<Review> _reviews;
        private readonly IUnitOfWork _unitOfWork;
        public ReviewRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _reviews = entitiesContext.Reviews;
            _unitOfWork = unitOfWork;
        }

        public double GetAverageRate(int VendorId)
        {
            IQueryable<Review> Data = _reviews.Where(r => r.Service.VendorId == VendorId);
            return Data.Any() ? Math.Round(Data.Select(r => r.Rate).Average()) : 0;
        }

        public HttpStatusCode Add(CreateReviewDTO createReviewDTO, string loggedInUserId)
        {
            Review review = createReviewDTO.ToEntity(loggedInUserId);
            _reviews.Add(review);
            return _unitOfWork.SaveChanges();
        }

        public IQueryable<Review> GetByServiceId(int ServiceId)
        {
            return _reviews.Where(r => r.ServiceId == ServiceId);
        }

        public IQueryable<Review> GetByVendorId(int VendorId)
        {
            return _reviews.Where(r => r.Service.VendorId == VendorId);
        }

        public Review GetReviewById(int Id)
        {
            return _reviews.Where(p => p.Id == Id).FirstOrDefault()!;
        }

        public bool HasReviews(int ReservationId)
        {
            return _reviews.Where(r => r.ReservationId == ReservationId).Any();
        }

        public PaginationDTO<ReviewWithServiceDTO> GetPaginatedReviewsByVendorId(int VendorId, int PageIndex, int PageSize)
        {
            return _reviews.Where(r => r.Service.VendorId == VendorId).Select(r => r.ToReviewWithServiceDTO()).ToPaginationDTO(PageIndex, PageSize);
        }
        public HttpStatusCode Delete(int reviewId, string userId)
        {
            Review? review = _reviews.Where(r => r.Id == reviewId && r.UserId == userId).FirstOrDefault();
            if (review == null) return HttpStatusCode.NotFound;
            _reviews.Remove(review);
            return _unitOfWork.SaveChanges();
        }
    }
}

