using Models;
namespace ViewModels.UserViewModels
{
    public static class RegisterationToUser
    {
        public static User ToUser(this RegisterationViewModel viewModel)
        {
            return new User
            {
                Name = viewModel.Name,
                NationalID = viewModel.NationalID,
                PicUrl = viewModel.PicURL,
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Gender = viewModel.Gender
            };
        }
    }

}

