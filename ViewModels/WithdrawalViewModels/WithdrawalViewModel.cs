using System;
using ViewModels.ReservationViewModels;
using ViewModels.PaymentViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.WithdrawalViewModels
{
    public class WithdrawalViewModel
    {
        public required string InstaPay { get; set; }
        public required DateTime DateTime { get; set; }
        public required IEnumerable<PaymentViewModel> Payments { get; set; }
        public required VendorMinInfoViewModel Vendor { get; set; }
    }
}

