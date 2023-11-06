using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class PostManager : MainManager<Post>
    {
        private readonly EntitiesContext EntitiesContext;
        public PostManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Post> Add(Post Entity)
        {
            return EntitiesContext.Add(Entity);
        }
        public Post? GetPostByID(int ID)
        {
            return Get(ID).FirstOrDefault();
        }

        public IQueryable<Post> GetByVendorID(int VendorId)
        {
            return Get().Where(p => p.Vendor.Id == VendorId);
        }

        public EntityEntry<Post> Update(Post Entity)
        {
            return EntitiesContext.Update(Entity);
        }
    }
}

