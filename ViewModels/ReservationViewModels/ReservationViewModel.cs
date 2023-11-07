using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ServiceViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public class ReservationViewModel
    {
        public required string UserId { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public char Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateTime { get; set; }
        public required VendorMinInfoViewModel Vendor { get; set; }
    }
}
