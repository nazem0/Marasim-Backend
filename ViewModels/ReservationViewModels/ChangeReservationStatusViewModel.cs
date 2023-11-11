using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ReservationViewModels
{
    public class ChangeReservationStatusViewModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
    }

    public class UserChangeReservationStatusViewModel
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
    }
}
