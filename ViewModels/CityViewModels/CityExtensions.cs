using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CityViewModels
{
    public static class CityExtensions
    {
        public static CityViewModel ToCityViewModel(this City Data)
        {
            return new CityViewModel
            {
                Id = Data.Id,
                GovId = Data.GovernorateId,
                NameAr = Data.NameAr,
                NameEn = Data.NameEn,
            };
        }
    }
}
