using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.GenerationViewModels
{
    public class GenerateVendorViewModel
    {
        [Required(ErrorMessage ="يجب إدخال فئة الخدمة")]
        public required int CategoryId { get; set; }
        public int? GovernorateId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public float? Price { get; set; } = null;
        public int? Rate { get; set; } = null;
    }
}
