using Models;

namespace ViewModels.PromoCodeViewModels
{
    public static class PromoCodeExtensions
    {
        public static PromoCode ToModel(this CreatePromoCodeViewModel Data)
        {
            return new PromoCode()
            {
                Code = Data.Code,
                Discount = Data.Discount,
                Limit = Data.Limit,
                ServiceId = Data.ServiceId,
                Count = Data.Count,
                StartDate = DateTime.Now,
                ExpirationDate = Data.ExpirationDate,
            };
        }
        public static PromoCodeViewModel ToPromoCodeViewModel(this PromoCode Data)
        {
            return new PromoCodeViewModel
            {
                Code = Data.Code,
                Count = Data.Count,
                Discount = Data.Discount,
                ExpirationDate = Data.ExpirationDate,
                StartDate = DateTime.Now,
            };
        }
    }
}