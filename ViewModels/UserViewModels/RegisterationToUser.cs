using Models;

namespace ViewModels.UserViewModels
{
    public static class RegistrationToUser
    {
        public static User ToUser(this IUserRegisteration viewModel)
        {
            return new User
            {
                Name = viewModel.Name,
                NationalId = viewModel.NationalId,
                PicUrl = viewModel.PicUrl,
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Gender = viewModel.Gender,
                RegistrationDate = DateTime.Now,
            };
        }
    }

}

