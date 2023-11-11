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
        public Post? GetPostById(int Id)
        {
            return Get(Id).FirstOrDefault();
        }

        public IQueryable<Post> GetByVendorId(int VendorId)
        {
            return Get().Where(p => p.Vendor.Id == VendorId);
        }

        public EntityEntry<Post> Update(Post Entity)
        {
            return EntitiesContext.Update(Entity);
        }

        public void Delete(int PostId)
        {
            Post? Post = Get(PostId).FirstOrDefault();
            if (Post != null)
            {
                base.Delete(Post);
            }
            else
            {
                throw new Exception("Post Is Not Found");
            }
        }
    }
}

