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
            };
        }
    }
}
