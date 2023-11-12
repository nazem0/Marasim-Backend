using System.ComponentModel.DataAnnotations;

namespace ViewModels.PaymentViewModel
{
    public class AddPaymentViewModel
    {
        [Required]
        public required string InstaPay { get; set; }
        [Required]
        public int ReservationId { get; set; }
    }
}
