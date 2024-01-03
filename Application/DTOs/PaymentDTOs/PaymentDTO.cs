using Application.DTOs.ReservationDTOs;

namespace Application.DTOs.PaymentDTOs
{
    public class PaymentDTO
    {
        public required string InstaPay { get; set; }
        public required float Amount { get; set; }
        public required DateTime DateTime { get; set; }
        public required AdminReservationDTO Reservation { get; set; }
    }
}
