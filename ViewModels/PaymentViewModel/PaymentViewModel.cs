using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.PaymentViewModel
{
    public class PaymentViewModel
    {
        public required string InstaPay { get; set; }
        public float ServicePrice { get; set; }
        public required UserMinInfoViewModel User { get; set; }
        public required VendorMinInfoViewModel Vendor { get; set; }
    }
}
