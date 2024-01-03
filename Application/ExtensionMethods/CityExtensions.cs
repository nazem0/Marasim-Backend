using Application.DTOs.CityDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class CityExtensions
    {
        public static CityDTO ToCityDTO(this City city)
        {
            return new CityDTO
            {
                Id = city.Id,
                GovId = city.GovernorateId,
                NameAr = city.NameAr,
                NameEn = city.NameEn,
            };
        }
    }
}
