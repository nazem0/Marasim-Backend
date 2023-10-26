using Models;

namespace ViewModels.UserViewModels
{
    public static class RegisterationToUser
    {
        public static User ToUser(this IUserRegisteration viewModel)
        {
            return new User
            {
                Name = viewModel.Name,
                NationalID = viewModel.NationalID,
                PicUrl = viewModel.PicUrl,
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Gender = viewModel.Gender
            };
        }
    }

}

