using Models;
using System.Linq.Expressions;
using ViewModels.CategoryViewModels;
using ViewModels.CityViewModels;
using ViewModels.FollowViewModels;
using ViewModels.GovernorateViewModels;
using ViewModels.PostViewModels;
using ViewModels.ServiceViewModels;

namespace ViewModels.VendorViewModels
{
    public static class VendorExtensions
    {
        public static Vendor ToVendor(this VendorRegistrationViewModel viewModel, User _User)
        {
            return new Vendor
            {
                User = _User,
                GovernorateId = viewModel.GovernorateId,
                CityId = viewModel.CityId,
                Street = viewModel.Street,
                District = viewModel.District,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                Summary = viewModel.Summary,
                CategoryId = viewModel.CategoryId,
                ExternalUrl = viewModel.ExternalUrl,
            };
        }

        public static VendorViewModel ToVendorViewModel(this Vendor Vendor, User _User)
        {
            return new VendorViewModel
            {
                Id = Vendor.Id,
                District = Vendor.District,
                City = Vendor.City.ToCityViewModel(),
                Governorate = Vendor.Governorate.ToGovernorateViewModel(),
                Latitude = Vendor.Latitude,
                Longitude = Vendor.Longitude,
                Summary = Vendor.Summary,
                CategoryId = Vendor.CategoryId,
                ExternalUrl = Vendor.ExternalUrl,
                UserId = Vendor.UserId,
                Name = _User.Name,
                Gender = _User.Gender,
                NationalId = _User.NationalId,
                PhoneNumber = _User.PhoneNumber!,
                PicUrl = _User.PicUrl
            };
        }

        public static VendorFullViewModel ToVendorFullViewModel(this Vendor Vendor, User _User)
        {
            return new VendorFullViewModel
            {
                Id = Vendor.Id,
                Governorate = Vendor.Governorate.ToGovernorateViewModel(),
                City = Vendor.City.ToCityViewModel(),
                District = Vendor.District,
                Street = Vendor.Street,
                Latitude = Vendor.Latitude,
                Longitude = Vendor.Longitude,
                Summary = Vendor.Summary,
                CategoryId = Vendor.CategoryId,
                ExternalUrl = Vendor.ExternalUrl,
                UserId = Vendor.UserId,
                Name = _User.Name,
                Gender = _User.Gender,
                NationalId = _User.NationalId,
                PhoneNumber = _User.PhoneNumber!,
                PicUrl = _User.PicUrl,
                Followers = Vendor.Followers.Select(f => f.ToFollowerViewModel()),
                Services = Vendor.Services.Select(s => s.ToServicePartialViewModel()),
                Posts = Vendor.Posts.Select(p => p.ToViewModel())
            };
        }
        public static VendorMinInfoViewModel ToVendorMinInfoViewModel(this Vendor Data)
        {
            return new VendorMinInfoViewModel
            {
                Id = Data.Id,
                Name = Data.User.Name,
                PicUrl = Data.User.PicUrl,
                UserId = Data.UserId,
                PhoneNumber = Data.User.PhoneNumber!
            };
        }

        public static VendorMidInfoViewModel ToVendorMidInfoViewModel(this Vendor Data)
        {
            return new VendorMidInfoViewModel
            {
                Id = Data.Id,
                Name = Data.User.Name,
                PicUrl = Data.User.PicUrl,
                UserId = Data.UserId,
                Category = Data.Category.ToCategoryNameViewModel().Name,
                Summary = Data.Summary,
                City = Data.City.NameAr,
                Governorate = Data.Governorate.NameAr,
                District = Data.District,
                Street = Data.Street,
                RegistrationDate = Data.User.RegistrationDate
            };
        }

        public static GeneratedVendorViewModel ToGeneratedVendorViewModel(this Vendor Data)
        {
            return new GeneratedVendorViewModel
            {
                Id = Data.Id,
                Category = Data.Category.ToCategoryNameViewModel().Name,
                City = Data.City.NameAr,
                District = Data.District,
                Governorate = Data.Governorate.NameAr,
                Name = Data.User.Name,
                PicUrl = Data.User.PicUrl,
                Services = Data.Services.Select(s => s.ToServicePartialViewModel()),
                Street = Data.Street,
                Summary = Data.Summary,
                UserId = Data.UserId
            };
        }
    }
}
