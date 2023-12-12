using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.ReviewViewModels;

namespace Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        private readonly EntitiesContext EntitiesContext;
        public ReviewRepository(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public double GetAverageRate(int VendorId)
        {
            IQueryable<Review> Data = Get().Where(r => r.Service.VendorId == VendorId);
            if (Data.Any())
            {
                Data.Select(r => r.Rate).Average();
                return Math.Round(Data.Select(r => r.Rate).Average());
            }
            else
            {
                return 0;
            }
        }

        public EntityEntry<Review> Add(Review Entity)
        {
            return EntitiesContext.Add(Entity);
        }

        public IQueryable<Review> GetByServiceId(int ServiceId)
        {
            return Get().Where(r => r.ServiceId == ServiceId);
        }

        public IQueryable<Review> GetByVendorId(int VendorId)
        {
            return Get().Where(r => r.Service.VendorId == VendorId);
        }

        public Review GetReviewById(int Id)
        {
            return Get().Where(p => p.Id == Id).FirstOrDefault()!;
        }

        public EntityEntry<Review> Update(Review Entity)
        {
            return EntitiesContext.Update(Entity);
        }

        public bool HasReviews(int ReservationId)
        {
            return Get().Where(r => r.ReservationId == ReservationId).Any();
        }

        public PaginationViewModel<ReviewFullViewModel> GetPaginatedReviewsByVendorId(int VendorId, int PageSize, int PageIndex)
        {
            var data = base.Filter(r => r.Service.VendorId == VendorId, PageSize, PageIndex)
                .Select(p => p.ToReviewFullViewModel());
            int Count = Get().Where(r => r.Service.VendorId == VendorId).Count();
            int Max = Convert.ToInt32(Math.Ceiling((double)Count / PageSize));
            return new PaginationViewModel<ReviewFullViewModel>
            {
                Data = data.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Count = Count,
                LastPage = Max
            };
        }
    }
}

