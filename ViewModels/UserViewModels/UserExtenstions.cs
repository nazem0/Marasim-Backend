using System;
using Models;
using ViewModels.VendorViewModels;

namespace ViewModels.UserViewModels
{
    public static class UserExtentions
    {
        public static UserDetails ToUserViewModel(this User User)
        {
            return new UserDetails
            {
                Id = User.Id,
                Name = User.Name,
                PicUrl = User.PicUrl,
                NationalId = User.NationalId,
                Gender = User.Gender,
                PhoneNumber = User.PhoneNumber!,
                Email = User.Email!
            };
        }
    }
}

