using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VendorGenerationDTOs
{
    public class GenerateVendorDTO
    {
        [Required(ErrorMessage = "يجب إدخال فئة الخدمة")]
        public required int CategoryId { get; set; }
        public required int GovernorateId { get; set; }
        public int? CityId { get; set; }
        public required float Price { get; set; }
        public int? Rate { get; set; } = null;
    }
}
