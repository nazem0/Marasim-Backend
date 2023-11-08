using Models;

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
            };
        }

        public static FollowViewModel ToViewModel(this Follow Follow, User User)
        {
            return new FollowViewModel
            {
                UserName = User.Name,
                DateTime = Follow.DateTime,

            };
        }
    }
}


    
  