using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.FollowViewModels;
using ViewModels.PostViewModels;

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

        public void Add(Follow Follow)
        {
            if (Follow == null)
            {
                throw new Exception("Follow Not Found");
            }
            else if (IsUserFollowingVendor(Follow.UserId, Follow.VendorId))
            {
                throw new Exception("Already Following");
            }
            else
            {
                EntitiesContext.Add(Follow);
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

        // not tested
        //public IQueryable GetPostsByFollow(string UserId)
        //{
        //    return Get()
        //        .Where(f => f.UserId == UserId)
        //        .Select(f => f.ToFollowPostsViewModel());
        //}
    }
}