using Models;
using ViewModels.VendorViewModels;

namespace ViewModels.CategoryViewModels
{
    public static class CategoryExtensions
    {
        public static Category ToCategory(this AddCategoryViewModel Data)
        {
            return new Category { Name = Data.Name };
        }
        public static CategoryViewModel ToCategoryViewModel(this Category Data)
        {
            return new CategoryViewModel { Id = Data.Id, Name = Data.Name, Vendors = Data.Vendors.Select(v => v.ToVendorMidInfoViewModel()) };
        }
        public static CategoryNameViewModel ToCategoryNameViewModel(this Category Data)
        {
            return new CategoryNameViewModel { Id = Data.Id, Name = Data.Name };
        }
        public static CategoriesWithMinMaxViewModel ToCategoriesWithMinMaxViewModel(this Category Data)
        {
            float Min = 0;
            float Max = 0;
            if (Data.Vendors.Where(v => v.Services.Count > 0).Any())
            {
                Min = Data.Vendors.SelectMany(v => v.Services).Min(s => s.Price);
                Max = Data.Vendors.SelectMany(v => v.Services).Max(s => s.Price);
            }
            return new CategoriesWithMinMaxViewModel
            {
                Id = Data.Id,
                Name = Data.Name,
                Min = Min,
                Max = Max
            };
        }
    }
}