using Application.DTOs.VendorDTOs;

namespace Application.DTOs.ReservationDTOs
{
    public class CheckoutReservationDTO
    {
        public DateTime DateTime { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Gov { get; set; }
        public required string District { get; set; }
        public required float Price { get; set; }
        public required string ServiceName { get; set; }
        public required virtual VendorMinInfoDTO Vendor { get; set; }

    }
}
