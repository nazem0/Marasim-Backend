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

        public IQueryable<Follow> GetFollowersVendor(int vendorId)
        {
            return Get().Where(f => f.VendorId == vendorId);
        }

        public IQueryable<Follow> GetFollowingForUser(string userId)
        {
            return Get().Where(f => f.UserId == userId);
        }

        public string Add(Follow follow)
        {
            if (IsUserFollowingVendor(follow.UserId, follow.VendorId))
            {
                return "You are already following this vendor.";
            }
            else
            {
                EntitiesContext.Add(follow);
                return "Followed";
            }
        }

        public bool IsUserFollowingVendor(string userId, int vendorId)
        {
            if (Get().Any(f => f.UserId == userId && f.VendorId == vendorId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Follow GetFollow(string userId, int vendorId)
        {
            if (Get().Any(f => f.UserId == userId && f.VendorId == vendorId))
            {
                return Get().Where(f => f.UserId == userId && f.VendorId == vendorId).FirstOrDefault()!;
            }
            else
            {
                return null!;
            }
        }

        public Follow? GetFollowByID(int ID)
        {
            return Get(ID).FirstOrDefault();
        }
    }
}