using Models;

namespace ViewModels.CategoryViewModels
{
    public static class CategoryExtensions
    {
        public static Category ToCategory(this AddCategoryViewModel Data)
        {
            return new Category { Name = Data.Name };
        }
    }
}
