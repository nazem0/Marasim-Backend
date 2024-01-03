using Application.DTOs.PromoCodeDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class PromoCodeExtensions
    {
        public static PromoCode ToEntity(this CreatePromoCodeDTO createPromoCodeDTO)
        {
            return new PromoCode()
            {
                Code = createPromoCodeDTO.Code,
                Discount = createPromoCodeDTO.Discount,
                Limit = createPromoCodeDTO.Limit,
                ServiceId = createPromoCodeDTO.ServiceId,
                Count = createPromoCodeDTO.Count,
                StartDate = DateTime.Now,
                ExpirationDate = createPromoCodeDTO.ExpirationDate,
            };
        }
        public static PromoCodeDTO? ToPromoCodeDTO(this PromoCode promoCode)
        {
            return new PromoCodeDTO
            {
                Code = promoCode.Code,
                Discount = promoCode.Discount,
                Count = promoCode.Count,
                ExpirationDate = promoCode.ExpirationDate,
                StartDate = promoCode.StartDate,
            };
        }
    }
}