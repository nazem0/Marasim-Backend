using System;
using Models;
using ViewModels.User;

public static class UserExtaintion
{
    public static User ToUser(this RegisterationViewModel viewModel)
    {
        return new User
        {
            Name = viewModel.Name,
            NationalID = viewModel.NationalID,
            PicUrl = "https://booking.webestica.com/assets/images/avatar/04.jpg",
            UserName = viewModel.Name.Replace(" ", ""),
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            Gender = viewModel.Gender
        };
    }
}


