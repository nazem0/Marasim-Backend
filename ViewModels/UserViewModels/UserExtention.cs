using Models;
namespace ViewModels.UserViewModels
{
    public static class UserExtention
    {
        public static User ToUser(this RegisterationViewModel viewModel)
        {
            return new User
            {
                Name = viewModel.Name,
                NationalID = viewModel.NationalID,
                PicUrl = "https://booking.webestica.com/assets/images/avatar/04.jpg",
                UserName = viewModel.Email,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                Gender = viewModel.Gender
            };
        }
    }

}

