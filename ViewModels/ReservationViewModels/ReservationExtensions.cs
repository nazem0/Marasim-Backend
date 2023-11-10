using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Latitude = Data.Latitude,
                Longitude = Data.Longitude,
                Address = Data.Address,
            };
        }

        public static ChangeReservationStatusViewModel ToReservation(this Reservation Data)
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
                Address = Data.Address,
                Latitude = Data.Latitude,
                Longitude = Data.Longitude,
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel()
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
                Address = Data.Address,
                Latitude = Data.Latitude,
                Longitude = Data.Longitude,
                User = Data.User.ToUserViewModel()
            };
        }
        public static CheckoutReservationViewModel ToCheckoutReservationViewModel(this Reservation Data)
        {
            return new CheckoutReservationViewModel
            {
                Address = Data.Address,
                ServiceName = Data.Service.Title,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel()
            };
        }
    }
}
