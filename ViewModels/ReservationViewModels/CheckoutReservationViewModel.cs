using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public class CheckoutReservationViewModel
    {
        public DateTime DateTime { get; set; }
        public required string Address { get; set; }
        public float Price { get; set; }
        public required string ServiceName { get; set; }
        public required virtual VendorMinInfoViewModel Vendor { get; set; }

    }
}
