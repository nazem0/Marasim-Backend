using ViewModels.VendorViewModels;

namespace ViewModels.ReservationViewModels
{
    public class UserReservationViewModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public char Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public required string Address { get; set; }
        public required VendorMinInfoViewModel Vendor { get; set; }
    }
}
