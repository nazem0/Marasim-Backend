using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.PaymentViewModel
{
    public static class PaymentExtensions
    {
        public static Payment ToPayment(this AddPaymentViewModel Data)
        {
            return new Payment
            {
                InstaPay = Data.InstaPay,
                ReservationId = Data.ReservationId,
            };
        }

        public static PaymentViewModel ToPaymentViewModel(this Payment Data)
        {
            return new PaymentViewModel
            {
                InstaPay = Data.InstaPay,
                ServicePrice= Data.Reservation.Price,
                Vendor = Data.Reservation.Service.Vendor.ToVendorMinInfoViewModel(),
                User = Data.Reservation.User.ToUserMinInfoViewModel()
            };
        }
    }
}
