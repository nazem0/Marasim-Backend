using Application.DTOs.CustomerDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class UserExtensions
    {
        public static CustomerDTO ToCustomerDTO(this User customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                PicUrl = customer.PicUrl,
                NationalId = customer.NationalId,
                Gender = customer.Gender,
                PhoneNumber = customer.PhoneNumber ?? "",
                Email = customer.Email ?? "",
                RegistrationDate = customer.RegistrationDate,
            };
        }
        public static CustomerMinInfoDTO ToCustomerMinInfoDTO(this User customer)
        {
            return new CustomerMinInfoDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                PicUrl = customer.PicUrl,
            };
        }
    }
}

