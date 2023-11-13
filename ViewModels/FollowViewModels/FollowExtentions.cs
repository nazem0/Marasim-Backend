using Models;
using ViewModels.PostViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.FollowViewModels
{
    public static class FollowExtentions
    {
        public static Follow ToEntity(this AddFollowViewModel addFollowViewModel, string UserId)
        {
            return new Follow
            {
                UserId = UserId,
                VendorId = addFollowViewModel.VendorId,
                DateTime = DateTime.Now
            };
        }

        public static FollowViewModel ToFollowerViewModel(this Follow Follow)
        {
            return new FollowViewModel
            {
                Name = Follow.User.Name,
                UserId = Follow.User.Id,
                PicUrl = Follow.User.PicUrl,
                DateTime = Follow.DateTime,
            };
        }

        public static FollowingViewModel ToFollowingViewModel(this Follow Follow)
        {
            return new FollowingViewModel
            {
                Name = Follow.Vendor.User.Name,
                PicUrl = Follow.Vendor.User.PicUrl,
                UserId = Follow.Vendor.UserId,
                VendorId = Follow.Vendor.Id,
                DateTime = Follow.DateTime
            };
        }

        public static FollowPostsViewModel ToFollowPostsViewModel(this Follow Follow)
        {
            return new FollowPostsViewModel
            {
                Vendor = Follow.Vendor.ToVendorMidInfoViewModel(),
                Posts = Follow.Vendor.Posts.Select(p => p.ToViewModel())
            };
        }
    }
}



