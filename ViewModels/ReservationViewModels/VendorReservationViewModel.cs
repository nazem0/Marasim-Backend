using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ServiceViewModels;
using ViewModels.UserViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public class VendorReservationViewModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public char Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateTime { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Gov { get; set; }
        public required string District { get; set; }
        public required UserViewModel User { get; set; }
        public required ServiceMinInfoViewModel Service { get; set; }
    }
}
