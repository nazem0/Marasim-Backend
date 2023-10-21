using Models;

namespace Repository
{
    public class CategoryManager : MainManager<Category>
    {
        public CategoryManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }
    }
}