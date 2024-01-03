using Application.DTOs.AccountDTOs;
using Application.DTOs.VendorDTOs;
using Domain.Entities;
using ViewModels.VendorViewModels;

namespace Application.ExtensionMethods
{
    public static class VendorExtensions
    {
        public static Vendor ToEntity(this VendorRegisterDTO vendorRegisterDTO, User user)
        {
            vendorRegisterDTO.ToEntity();
            return new Vendor
            {
                User = user,
                GovernorateId = vendorRegisterDTO.GovernorateId,
                CityId = vendorRegisterDTO.CityId,
                Street = vendorRegisterDTO.Street,
                District = vendorRegisterDTO.District,
                Summary = vendorRegisterDTO.Summary,
                CategoryId = vendorRegisterDTO.CategoryId,
                ExternalUrl = vendorRegisterDTO.ExternalUrl ?? "",
            };
        }

        public static VendorDTO ToVendorDTO(this Vendor vendor)
        {
            return new VendorDTO
            {
                Id = vendor.Id,
                District = vendor.District,
                City = vendor.City.ToCityDTO(),
                Governorate = vendor.Governorate.ToGovernorateDTO(),
                Summary = vendor.Summary,
                CategoryId = vendor.CategoryId,
                ExternalUrl = vendor.ExternalUrl,
                UserId = vendor.UserId,
                Name = vendor.User.Name,
                Gender = vendor.User.Gender,
                NationalId = vendor.User.NationalId,
                PhoneNumber = vendor.User.PhoneNumber!,
                PicUrl = vendor.User.PicUrl
            };
        }

        public static VendorPageDTO ToVendorPageDTO(this Vendor vendor)
        {
            return new VendorPageDTO
            {
                Id = vendor.Id,
                Governorate = vendor.Governorate.ToGovernorateDTO(),
                City = vendor.City.ToCityDTO(),
                District = vendor.District,
                Street = vendor.Street,
                Summary = vendor.Summary,
                CategoryId = vendor.CategoryId,
                ExternalUrl = vendor.ExternalUrl,
                UserId = vendor.UserId,
                Name = vendor.User.Name,
                Gender = vendor.User.Gender,
                NationalId = vendor.User.NationalId,
                PhoneNumber = vendor.User.PhoneNumber!,
                PicUrl = vendor.User.PicUrl,
                Followers = vendor.Followers.OrderByDescending(f=>f.DateTime).Select(f => f.ToVendorFollowerDTO()),
                Services = vendor.Services.Where(s=>s.IsDeleted==false).Select(s => s.ToCustomerServiceDTO()),
                Posts = vendor.Posts.OrderByDescending(p=>p.DateTime).Select(p => p.ToPostDTO())
            };
        }
        public static VendorMinInfoDTO ToVendorMinInfoDTO(this Vendor vendor)
        {
            return new VendorMinInfoDTO
            {
                Id = vendor.Id,
                Name = vendor.User.Name,
                PicUrl = vendor.User.PicUrl,
                UserId = vendor.UserId,
                PhoneNumber = vendor.User.PhoneNumber!
            };
        }

        public static VendorMidInfoDTO ToVendorMidInfoDTO(this Vendor vendor)
        {
            return new VendorMidInfoDTO
            {
                Id = vendor.Id,
                Name = vendor.User.Name,
                PicUrl = vendor.User.PicUrl,
                UserId = vendor.UserId,
                Category = vendor.Category.ToCategoryDTO().Name,
                Summary = vendor.Summary,
                City = vendor.City.NameAr,
                Governorate = vendor.Governorate.NameAr,
                District = vendor.District,
                Street = vendor.Street,
                RegistrationDate = vendor.User.RegistrationDate
            };
        }

        public static GeneratedVendorDTO ToGeneratedVendorViewModel(this Vendor vendor)
        {
            return new GeneratedVendorDTO
            {
                Id = vendor.Id,
                Category = vendor.Category.ToCategoryDTO().Name,
                City = vendor.City.NameAr,
                District = vendor.District,
                Governorate = vendor.Governorate.NameAr,
                Name = vendor.User.Name,
                PicUrl = vendor.User.PicUrl,
                Services = vendor.Services.Select(s => s.ToCustomerServiceDTO()),
                Street = vendor.Street,
                Summary = vendor.Summary,
                UserId = vendor.UserId
            };
        }
    }
}
