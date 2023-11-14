using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.GenerationViewModels
{
    public class GeneratePackageViewModel
    {
        public required float Budget { get; set; }
        public required int[] Categories { get; set; }
        public int? GovId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public int? Rate { get; set; } = null;
    }
}
