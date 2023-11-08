using Models;

namespace ViewModels.UserViewModels
{
    public static class UserExtensions
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
        public static UserMinInfoViewModel ToUserMinInfoViewModel(this User User)
        {
            return new UserMinInfoViewModel
            {
                Id = User.Id,
                Name = User.Name,
                PicUrl = User.PicUrl,
            };
        }
    }
}

