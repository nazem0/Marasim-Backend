using Models;
using ViewModels.ReservationViewModels;

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
    }
}
