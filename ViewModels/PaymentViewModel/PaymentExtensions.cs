using Models;
using ViewModels.ReservationViewModels;
using ViewModels.ServiceViewModels;
using ViewModels.UserViewModels;

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
                DateTime = Data.DateTime,
                Reservation = Data.Reservation.ToAdminReservationViewModel()
            };
        }
        public static VendorPaymentViewModel ToVendorPaymentViewModel(this Payment Data)
        {
            return new VendorPaymentViewModel
            {
                DateTime = Data.DateTime,
                Price = (double)Data.Reservation.Price*0.3,
                ServiceTitle = Data.Reservation.Service.Title,
                User = Data.Reservation.User.ToUserMinInfoViewModel(),
            };
        }
    }
}
