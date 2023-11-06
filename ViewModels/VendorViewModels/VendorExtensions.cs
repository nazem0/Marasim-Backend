using Models;

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
                Address = Vendor.Address,
                Latitude = Vendor.Latitude,
                Longitude = Vendor.Longitude,
                Summary = Vendor.Summary,
                CategoryId = Vendor.CategoryId,
                ExternalUrl = Vendor.ExternalUrl,
                UserID = Vendor.UserID,
                Name = _User.Name,
                Gender = _User.Gender,
                NationalID = _User.NationalID,
                PicUrl = _User.PicUrl
            };
        }
    }
}
