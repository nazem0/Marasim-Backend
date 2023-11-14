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
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Gov { get; set; }
        public required string District { get; set; }
        public required float Price { get; set; }
        public required string ServiceName { get; set; }
        public required virtual VendorMinInfoViewModel Vendor { get; set; }

    }
}
