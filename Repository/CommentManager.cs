using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class CommentManager : MainManager<Comment>
    {
        private readonly EntitiesContext EntitiesContext;
        public CommentManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Comment> Add(Comment comment)
        {
            return EntitiesContext.Add(comment);
        }
        public IQueryable<Comment> GetByPostID(int PostId)
        {
            return Get().Where(c => c.PostId == PostId);
        }
    }
}

