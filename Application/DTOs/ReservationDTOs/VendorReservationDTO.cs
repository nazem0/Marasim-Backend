using Application.DTOs.CustomerDTOs;
using Application.DTOs.ServiceDTOs;

namespace Application.DTOs.ReservationDTOs
{
    public class VendorReservationDTO
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
        public required CustomerDTO User { get; set; }
        public required VendorServiceDTO Service { get; set; }
    }
}
