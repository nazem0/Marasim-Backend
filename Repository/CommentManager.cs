using Models;

namespace Repository
{
    public class CommentManager : MainManager<Comment>
    {
        public CommentManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public IQueryable<Comment> GetByPostID(int PostID)
        {
            return Get().Where(c => c.PostID == PostID);
        }
    }
}

