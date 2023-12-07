using ViewModels.ReservationViewModels;

namespace ViewModels.PaymentViewModels
{
    public class PaymentViewModel
    {
        public required string InstaPay { get; set; }
        public required float Amount { get; set; }
        public required DateTime DateTime { get; set; }
        public required AdminReservationViewModel Reservation { get; set; }
    }
}
