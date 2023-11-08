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
                DateTime = Data.DateTime
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
                User = Data.User.ToUserViewModel()
            };
        }
    }
}
