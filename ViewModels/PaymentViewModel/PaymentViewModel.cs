using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ReservationViewModels;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.PaymentViewModel
{
    public class PaymentViewModel
    {
        public required string InstaPay { get; set; }
        public required DateTime DateTime { get; set; }
        public required AdminReservationViewModel Reservation { get;set; }
    }
}
