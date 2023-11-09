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
            return new CategoryViewModel { Id = Data.Id, Name = Data.Name, Vendors = Data.Vendors.Select(v => v.ToVendorViewModel(v.User)) };
        }
        public static CategoryNameViewModel ToCategoryNameViewModel(this Category Data)
        {
            return new CategoryNameViewModel { Id = Data.Id, Name = Data.Name };
        }
    }
}