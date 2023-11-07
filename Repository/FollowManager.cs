using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class FollowManager : MainManager<Follow>
    {
        private readonly EntitiesContext EntitiesContext;
        public FollowManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public IQueryable<Follow> GetFollowersForVendor(int vendorId)
        {
            return Get().Where(f => f.VendorId == vendorId);
        }

        public IQueryable<Follow> GetFollowingForUser(string userId)
        {
            return Get().Where(f => f.UserId == userId);
        }

        public EntityEntry<Follow> Add(Follow follow)
        {
            return EntitiesContext.Add(follow);
        }
       
        public bool IsUserFollowingVendor(string userId, int vendorId)
        {
            return Get().Any(f => f.UserId == userId && f.VendorId == vendorId);
        }


        public Follow? GetFollowByID(int ID)
        {
           return Get(ID).FirstOrDefault();

        }


    }
}

 



       

    