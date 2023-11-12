using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReservationViewModels
{
    public class AddReservationViewModel
    {
        [Required]
        public required string UserId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [MaxLength(10)]
        public string? PromoCode { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public required string Address { get; set; }
    }
}
