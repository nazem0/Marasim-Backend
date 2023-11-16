using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.UserViewModels;

namespace ViewModels.PaymentViewModel
{
    public class VendorPaymentViewModel
    {
        public required int ServiceId { get; set; }
        public required float Price { get; set; }
        public required DateTime DateTime { get; set; }
        public required UserMinInfoViewModel User { get; set; }

    }
}
