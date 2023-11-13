using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.GovernorateViewModels
{
    public static class GovernorateExtensions
    {
        public static GovernorateViewModel ToGovernorateViewModel(this Governorate model)
        {
            return new GovernorateViewModel
            {
                Id = model.Id,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
            };
        }
    }
}
