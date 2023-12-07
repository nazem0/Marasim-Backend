using System.ComponentModel.DataAnnotations;

namespace ViewModels.PaymentViewModels
{
    public class AddPaymentViewModel
    {
        [Required]
        public required string InstaPay { get; set; }

        [Required]
        public required float Amount { get; set; }

        [Required]
        public int ReservationId { get; set; }
    }
}
