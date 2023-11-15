using Models;

namespace ViewModels.UserViewModels
{
    public static class UserExtensions
    {
        public static UserViewModel ToUserViewModel(this User User)
        {
            return new UserViewModel
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

