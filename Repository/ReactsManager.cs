using Models;

namespace Repository
{
    public class ReactsManager : MainManager<React>
    {
        public ReactsManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public IQueryable<React> GetByPostID(int PostID)
        {
            return Get().Where(r => r.PostID == PostID);
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