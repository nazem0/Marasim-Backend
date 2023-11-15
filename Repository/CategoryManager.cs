using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.CategoryViewModels;

namespace Repository
{
    public class CategoryManager : MainManager<Category>
    {
        private readonly EntitiesContext EntitesContext;
        public CategoryManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitesContext = _dBContext;
        }
        public EntityEntry<Category>? Add(AddCategoryViewModel Data)
        {
            if (EntitesContext.Set<Category>().Where(c => c.Name == Data.Name).Any())
            {
                return null;
            }
            else
                return EntitesContext.Add(Data.ToCategory());
        }
        public new IQueryable<CategoryViewModel> Get()
        {
            return
                EntitesContext.Categories
                .Select(C => C.ToCategoryViewModel());
        }

        public Category GetByVendorId(int VendorId)
        {
            return EntitesContext.Categories.Where(c => c.Vendors.Any(v => v.Id == VendorId)).FirstOrDefault()!;
        }

        public IEnumerable<CategoryNameViewModel> GetNames()
        {
            return EntitesContext.Categories.Select(c => c.ToCategoryNameViewModel());
        }
    }
}