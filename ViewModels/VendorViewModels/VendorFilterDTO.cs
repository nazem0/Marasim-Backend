using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.VendorViewModels
{
    public class VendorFilterDTO
    {
        public string? Name { get; set; }
        public int? CityId { get; set; }
        public int? GovernorateId { get; set; }
        public string? District { get; set; }
        public string? Categories { get; set; }
    }
}
