using Application.DTOs.CategoryDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class CategoryExtensions
    {
        public static Category ToEntity(this CreateCategoryDTO createCategoryDTO)
        {
            return new Category { Name = createCategoryDTO.Name };
        }
        public static CategoryWithVendorsDTO ToCategoryWithVendorsDTO(this Category category)
        {
            return new CategoryWithVendorsDTO
            {
                Id = category.Id,
                Name = category.Name,
                Vendors =
                category.Vendors.OrderByDescending(v => v.Services.SelectMany(v => v.Reservations).Count())
                .Take(3)
                .Select(v => v.ToVendorMidInfoDTO())
            };
        }
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO { Id = category.Id, Name = category.Name };
        }
        public static CategoryMaxMinPriceDTO ToCategoryMaxMinPriceDTO(this Category category)
        {
            float Min = 0, Max = 0;
            if (category.Vendors.Where(v => v.Services.Count > 0).Any())
            {
                Min = category.Vendors.SelectMany(v => v.Services).Min(s => s.Price);
                Max = category.Vendors.SelectMany(v => v.Services).Max(s => s.Price);
            }
            return new CategoryMaxMinPriceDTO { Id = category.Id, Name = category.Name, Min = Min, Max = Max };
        }
    }
}