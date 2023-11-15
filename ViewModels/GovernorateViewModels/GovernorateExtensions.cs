using Models;

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
