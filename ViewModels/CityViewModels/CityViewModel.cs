using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CityViewModels
{
    public class CityViewModel
    {
        public required int Id { get; set; }
        public required int GovId { get; set; }
        public required string NameAr { get; set; }
        public required string NameEn { get; set; }

    }
}
