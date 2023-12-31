﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ReservationDTOs
{
    public class CreateReservationDTO
    {
        [Required]
        public required string UserId { get; set; }
        [Required]
        public required int ServiceId { get; set; }
        [Required]
        public required DateTime DateTime { get; set; }
        [MaxLength(10)]
        public string? PromoCode { get; set; }
        [Required(ErrorMessage = "يجب تحديد المدينة")]
        public required int CityId { get; set; }
        [Required(ErrorMessage = "يجب تحديد المحافظة")]
        public required int GovId { get; set; }
        [Required, MaxLength(100, ErrorMessage = "لا يجب ان يتعدى اسم المنطقة المئة حرف")]
        public required string District { get; set; }
        [MaxLength(100, ErrorMessage = "لا يجب ان يتعدى اسم الشارع المئة حرف")]
        public string? Street { get; set; }
    }
}
