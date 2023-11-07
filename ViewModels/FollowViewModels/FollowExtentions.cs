using Models;
using ViewModeles;
using ViewModels.PostViewModels;

namespace ViewModels.PostViewModels
{
    public static class FollowExtentions
    {
    

    

        public static Follow ToEntity(this AddFollowViewModel addFollowViewModel, string UserId)
        {
            return new Follow
            {
                UserId = UserId,
                VendorId = addFollowViewModel.VendorId,
            };
        }


        public static FollowViewModel ToViewModel(this Follow follow)
        {
            return new FollowViewModel
            {
                Id = follow.Id,
                UserId = follow.UserId,
                VendorId = follow.VendorId,
            };
        }
    }
}


    
  