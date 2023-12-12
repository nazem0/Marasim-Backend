using Models;
using ViewModels.FollowViewModels;
using ViewModels.PaginationViewModels;

namespace Repository
{
    public class FollowRepository : BaseRepository<Follow>
    {
        private readonly EntitiesContext EntitiesContext;
        public FollowRepository(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public PaginationViewModel<FollowViewModel> GetFollowersVendor(int VendorId, int PageSize, int PageIndex)
        {
            PaginationDTO<FollowViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Where(f => f.VendorId == VendorId)
                .Select(f => f.ToFollowerViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }

        public PaginationViewModel<FollowingViewModel> GetFollowingForUser(string UserId, int PageSize, int PageIndex)
        {
            PaginationDTO<FollowingViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Where(f => f.UserId == UserId)
                .Select(f => f.ToFollowingViewModel())
                .ToPaginationViewModel(PaginationDTO);
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
    }
}