using Application.DTOs.AccountDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class UserRegistrationExtensions
    {
        public static User ToEntity(this CustomerRegisterDTO customerRegisterDTO)
        {
            return new User
            {
                Name = customerRegisterDTO.Name,
                NationalId = customerRegisterDTO.NationalId,
                PicUrl = customerRegisterDTO.PicUrl,
                UserName = customerRegisterDTO.Email,
                Email = customerRegisterDTO.Email,
                PhoneNumber = customerRegisterDTO.PhoneNumber,
                Gender = customerRegisterDTO.Gender,
                RegistrationDate = DateTime.Now,
            };
        }
    }
}
