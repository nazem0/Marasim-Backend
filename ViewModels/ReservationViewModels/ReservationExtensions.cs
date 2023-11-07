using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static ReservationViewModel ToReservationViewModel(this Reservation Data)
        {
            return new ReservationViewModel
            {
                Id = Data.Id,
                UserId = Data.UserId,
                ServiceId = Data.ServiceId,
                DateTime = Data.DateTime,
                Price = Data.Price,
                Status = Data.Status,
                Vendor = Data.Service.Vendor.ToVendorMinInfoViewModel()
            };
        }
    }
}
