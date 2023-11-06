using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.CategoryViewModels;

namespace Repository
{
    public class CategoryManager : MainManager<Category>
    {
        public CategoryManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
        public EntityEntry<Category>? Add(AddCategoryViewModel Data)
        {
            if (Get().Where(c => c.Name == Data.Name).Any())
            {
                return null;
            }
            else
                return Add(Data.ToCategory());
        }
    }
}