using Models;
using ViewModels.ReviewViewModels;
using ViewModels.ServiceViewModels;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public static class ReservationExtensions
    {
        public static Reservation ToReservation(this AddReservationViewModel Data)
        {
            return new Reservation
            {
                UserId = Data.UserId,
                ServiceId = Data.ServiceId,
                DateTime = Data.DateTime,
                CityId = Data.CityId,
                GovernorateId = Data.GovId,
                Street = Data.Street,
                District = Data.District
            };
        }

        public static ChangeReservationStatusViewModel ToChangeReservationStatusViewModel(this Reservation Data)
        {
            return new ChangeReservationStatusViewModel
            {
                Id = Data.Id,
                VendorId = Data.Service.VendorId
            };
        }
        public static UserReservationViewModel ToUserReservationViewModel(this Reservation Data)
        {
            return new UserReservationViewModel
            {
                Id = Data.Id,
                ServiceId = Data.ServiceId,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Status = Data.Status,
                City = Data.City.NameAr,
                Gov = Data.Governorate.NameAr,
                Street = Data.Street,
                District = Data.District,
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel(),
                Review = Data.Review.ToReviewViewModel(),
                IsDeleted = Data.IsDeleted
            };
        }
        public static VendorReservationViewModel ToVendorReservationViewModel(this Reservation Data)
        {
            return new VendorReservationViewModel
            {
                Id = Data.Id,
                ServiceId = Data.ServiceId,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Status = Data.Status,
                City = Data.City.NameAr,
                Gov = Data.Governorate.NameAr,
                Street = Data.Street,
                District = Data.District,
                User = Data.User.ToUserViewModel(),
                Service = Data.Service.ToServiceMinInfoViewModel()
            };
        }
        public static CheckoutReservationViewModel ToCheckoutReservationViewModel(this Reservation Data)
        {
            return new CheckoutReservationViewModel
            {
                City = Data.City.NameAr,
                Gov = Data.Governorate.NameAr,
                Street = Data.Street,
                District = Data.District,
                ServiceName = Data.Service.Title,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel()
            };
        }
        public static AdminReservationViewModel ToAdminReservationViewModel(this Reservation Data)
        {
            return new AdminReservationViewModel
            {
                Id = Data.Id,
                ServiceId = Data.ServiceId,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Status = Data.Status,
                City = Data.City.NameAr,
                Gov = Data.Governorate.NameAr,
                Street = Data.Street,
                District = Data.District,
                User = Data.User.ToUserMinInfoViewModel(),
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel()
            };
        }
    }
}
