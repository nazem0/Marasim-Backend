using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ReviewDTOs
{
    public class CreateReviewDTO
    {
        [Required]
        public required int ServiceId { get; set; }
        [Required]
        public required int ReservationId { get; set; }
        [Required]
        public required int Rate { get; set; }

        [StringLength(1000)]
        public required string Message { get; set; }
    }
}

