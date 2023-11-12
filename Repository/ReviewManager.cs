using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ReviewViewModels;

namespace Repository
{
    public class ReviewManager : MainManager<Review>
    {
        private readonly EntitiesContext EntitiesContext;
        public ReviewManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
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
            return Get().Include(r => r.User)
                .Include(r => r.Service)
                .Where(r => r.Service.VendorId == VendorId);
                
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
    }
}

