using Models;

namespace Repository
{
    public class ReactManager : MainManager<React>
    {
        public ReactManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public IQueryable<React> GetByPostID(int PostID)
        {
            return Get().Where(r => r.PostID == PostID);
        }

        public bool isLiked(string UserID)
        {
            if (!Get().Any(r => r.UserID == UserID))
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