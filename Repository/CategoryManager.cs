using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.CategoryViewModels;

namespace Repository
{
    public class CategoryManager : MainManager<Category>
    {
        private readonly EntitiesContext EntitiesContext;
        public CategoryManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Category>? Add(AddCategoryViewModel Data)
        {
            if (EntitiesContext.Set<Category>().Where(c => c.Name == Data.Name).Any())
            {
                return null;
            }
            else
                return EntitiesContext.Add(Data.ToCategory());
        }
        public new IQueryable<CategoryViewModel> Get()
        {
            return
                EntitiesContext.Categories
                .Select(C => C.ToCategoryViewModel());
        }

        public Category GetByVendorId(int VendorId)
        {
            return base.Get().Where(c => c.Vendors.Any(v => v.Id == VendorId)).FirstOrDefault()!;
        }
        public IEnumerable<CategoryNameViewModel> GetNames()
        {
            return base.Get().Select(c => c.ToCategoryNameViewModel());
        }
        public IEnumerable<CategoriesWithMinMaxViewModel> GetCategoriesWithMinMax()
        {
            return base.Get().Select(c => c.ToCategoriesWithMinMaxViewModel());
        }
    }
}