using System.ComponentModel.DataAnnotations;

namespace ViewModels.GenerationViewModels
{
    public class GenerateVendorViewModel
    {
        [Required(ErrorMessage = "يجب إدخال فئة الخدمة")]
        public required int CategoryId { get; set; }
        public int? GovernorateId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public float? Price { get; set; } = null;
        public int? Rate { get; set; } = null;
    }
}
