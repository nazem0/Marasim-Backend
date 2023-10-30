using Models;

namespace Repository
{
    public class ReviewManager : MainManager<Review>
    {
        public ReviewManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public IQueryable<Review> GetByServiceID(int ServiceID)
        {
            return Get().Where(r => r.ServiceID == ServiceID);
        }

        public IQueryable<Review> GetByVendorID(int VendorID)
        {
            return Get().Where(r => r.Service.VendorID == VendorID);
        }

        public Review GetReviewByID(int ID)
        {
            return Get().Where(p => p.ID == ID).FirstOrDefault()!;
        }
    }
}

