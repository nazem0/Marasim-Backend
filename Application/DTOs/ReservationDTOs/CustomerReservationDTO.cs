using Application.DTOs.ReviewDTOs;
using Application.DTOs.VendorDTOs;

namespace Application.DTOs.ReservationDTOs
{
    public class CustomerReservationDTO
    {
        public required int Id { get; set; }
        public required int ServiceId { get; set; }
        public required float Price { get; set; }
        public required char Status { get; set; }
        public required bool IsDeleted { get; set; }
        public DateTime DateTime { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string Gov { get; set; }
        public required string District { get; set; }
        public ReviewDTO? Review { get; set; }
        public required VendorMinInfoDTO Vendor { get; set; }
    }
}
