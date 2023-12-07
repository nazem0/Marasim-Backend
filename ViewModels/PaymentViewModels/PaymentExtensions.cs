using Models;
using ViewModels.ReservationViewModels;
using ViewModels.UserViewModels;

namespace ViewModels.PaymentViewModels
{
    public static class PaymentExtensions
    {
        public static Payment ToPayment(this AddPaymentViewModel Data)
        {
            return new Payment
            {
                InstaPay = Data.InstaPay,
                Amount = (float)(Data.Amount * 0.3),
                ReservationId = Data.ReservationId,
                DateTime = DateTime.Now,
            };
        }

        public static PaymentViewModel ToPaymentViewModel(this Payment Data)
        {
            return new PaymentViewModel
            {
                InstaPay = Data.InstaPay,
                Amount = Data.Amount,
                DateTime = Data.DateTime,
                Reservation = Data.Reservation.ToAdminReservationViewModel()
            };
        }
        public static VendorPaymentViewModel ToVendorPaymentViewModel(this Payment Data)
        {
            return new VendorPaymentViewModel
            {
                DateTime = Data.DateTime,
                Amount = Data.Amount,
                ServiceTitle = Data.Reservation.Service.Title,
                User = Data.Reservation.User.ToUserMinInfoViewModel(),
            };
        }
    }
}
