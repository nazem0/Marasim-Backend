using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PaymentDTOs
{
    public class CreatePaymentDTO
    {
        [Required]
        public required string InstaPay { get; set; }

        [Required]
        public required float Amount { get; set; }

        [Required]
        public int ReservationId { get; set; }
    }
}
