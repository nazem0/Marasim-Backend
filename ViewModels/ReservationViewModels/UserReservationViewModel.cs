using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ReviewViewModels;
using ViewModels.ServiceViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public class UserReservationViewModel
    {
        public required int Id { get; set; }
        public required int ServiceId { get; set; }
        public required float Price { get; set; }
        public required char Status { get; set; }
        public required bool IsDeleted
        {
            get; set;
        }
        public DateTime DateTime { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Gov { get; set; }
        public required string District { get; set; }
        public ReviewViewModel? Review { get; set; }
        public required VendorMinInfoViewModel Vendor { get; set; }
    }
}
