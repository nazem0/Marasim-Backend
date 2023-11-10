using Models;

namespace ViewModels.PromoCodeViewModel
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
    }
}