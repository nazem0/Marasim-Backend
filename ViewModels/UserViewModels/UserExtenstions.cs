using System;
using Models;
using ViewModels.VendorViewModels;

namespace ViewModels.UserViewModels
{
    public static class UserExtentions
    {
        public static UserPublicDetails ToUserViewModel(this User User)
        {
            return new UserPublicDetails
            {
                Name = User.Name,
                PicUrl = User.PicUrl
            };
        }
    }
}

