using Models;
using System;
using System.Collections.Generic;
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
    }
}
