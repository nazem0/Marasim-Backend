using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class ReviewManager : MainManager<Review>
    {
        private readonly EntitiesContext EntitiesContext;
        public ReviewManager(EntitiesContext _dBContext) : base(_dBContext) {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Review> Add(Review Entity)
        {
            return EntitiesContext.Add(Entity);
        }
        public IQueryable<Review> GetByServiceID(int ServiceId)
        {
            return Get().Where(r => r.ServiceId == ServiceId);
        }

        public IQueryable<Review> GetByVendorID(int VendorId)
        {
            return Get().Where(r => r.Service.VendorID == VendorId);
        }

        public Review GetReviewByID(int Id)
        {
            return Get().Where(p => p.Id == Id).FirstOrDefault()!;
        }

        public EntityEntry<Review> Update(Review Entity)
        {
            return EntitiesContext.Update(Entity);
        }
    }
}

