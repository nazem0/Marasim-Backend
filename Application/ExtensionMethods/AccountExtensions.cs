using Application.DTOs.AccountDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class AccountExtensions
    {
        public static User ToEntity(this IUserRegister UserRegister)
        {
            return new User
            {
                Name = UserRegister.Name,
                NationalId = UserRegister.NationalId,
                PicUrl = UserRegister.PicUrl,
                UserName = UserRegister.Email,
                Email = UserRegister.Email,
                PhoneNumber = UserRegister.PhoneNumber,
                Gender = UserRegister.Gender,
                RegistrationDate = DateTime.Now,
            };
        }
    }
}
