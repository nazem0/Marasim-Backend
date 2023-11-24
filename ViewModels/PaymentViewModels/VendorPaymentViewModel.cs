using ViewModels.UserViewModels;

namespace ViewModels.PaymentViewModels
{
    public class VendorPaymentViewModel
    {
        public required double Price { get; set; }
        public required DateTime DateTime { get; set; }
        public required string ServiceTitle { get; set; }
        public required UserMinInfoViewModel User { get; set; }

    }
}
