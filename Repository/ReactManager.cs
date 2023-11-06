using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class ReactManager : MainManager<React>
    {
        private readonly EntitiesContext EntitiesContext;
        public ReactManager(EntitiesContext _dBContext) : base(_dBContext) {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<React> Add(React Entity)
        {
            return EntitiesContext.Add(Entity);
        }
        public IQueryable<React> GetByPostID(int PostID)
        {
            return Get().Where(r => r.PostId == PostID);
        }

        public bool IsLiked(string UserId)
        {
            if (!Get().Any(r => r.UserId == UserId))
            {
                return true;
            }
            else
                return false;
        }

        public void Delete(int ReactID)
        {
            React? React = Get(ReactID).FirstOrDefault();
            if (React != null)
            {
                Delete(React);
            }
            else
            {
                throw new Exception("React Not Found");
            }
        }
    }
}