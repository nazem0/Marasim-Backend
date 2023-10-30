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
                ServiceID = Data.ServiceID,
                Count = Data.Count,
                StartDate = Data.StartDate,
                ExpirationDate = Data.ExpirationDate,
            };
        }
    }
}