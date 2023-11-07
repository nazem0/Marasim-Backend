using Models;
using ViewModels.PostViewModels;
using ViewModels.ServiceViewModels;

namespace ViewModels.VendorViewModels
{
    public static class VendorExtensions
    {
        public static Vendor ToVendor(this VendorRegisterationViewModel viewModel, User _User)
        {
            return new Vendor
            {
                User = _User,
                Address = viewModel.Address,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                Summary = viewModel.Summary,
                CategoryId = viewModel.CategoryId
            };
        }

        public static VendorViewModel ToVendorViewModel(this Vendor Vendor, User _User)
        {
            return new VendorViewModel
            {
                Id = Vendor.Id,
                Address = Vendor.Address,
                Latitude = Vendor.Latitude,
                Longitude = Vendor.Longitude,
                Summary = Vendor.Summary,
                CategoryId = Vendor.CategoryId,
                ExternalUrl = Vendor.ExternalUrl,
                UserId = Vendor.UserId,
                Name = _User.Name,
                Gender = _User.Gender,
                NationalId = _User.NationalId,
                PicUrl = _User.PicUrl
            };
        }

        public static VendorFullViewModel ToVendorFullViewModel(this Vendor Vendor, User _User)
        {
            return new VendorFullViewModel
            {
                Id = Vendor.Id,
                Address = Vendor.Address,
                Latitude = Vendor.Latitude,
                Longitude = Vendor.Longitude,
                Summary = Vendor.Summary,
                CategoryId = Vendor.CategoryId,
                ExternalUrl = Vendor.ExternalUrl,
                UserId = Vendor.UserId,
                Name = _User.Name,
                Gender = _User.Gender,
                NationalId = _User.NationalId,
                PicUrl = _User.PicUrl,
                Services = Vendor.Services.Select(s => s.ToServicePartialViewModel()),
                Posts = Vendor.Posts.Select(p => p.ToPostPartialViewModel())
            };
        }
    }
}
