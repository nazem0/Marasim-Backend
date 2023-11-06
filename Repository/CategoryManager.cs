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
            if (Get().Where(c => c.Name == Data.Name).Any())
            {
                return null;
            }
            else
                return EntitesContext.Add(Data.ToCategory());
        }
        public new IQueryable<CategoryViewModel> Get()
        {
            return base.Get()
                .Include(C=>C.Vendors)
                .ThenInclude(V=>V.User)
                .Select(C => C.ToCategoryViewModel());
        }
    }
}