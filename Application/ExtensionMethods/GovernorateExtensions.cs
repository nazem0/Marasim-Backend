using Application.DTOs.GovernorateDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class GovernorateExtensions
    {
        public static GovernorateDTO ToGovernorateDTO(this Governorate governorate)
        {
            return new GovernorateDTO
            {
                Id = governorate.Id,
                NameAr = governorate.NameAr,
                NameEn = governorate.NameEn,
            };
        }
    }
}
