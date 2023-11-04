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

        public int CountReacts(int PostID)
        {
            return Get().Where(r => r.PostID == PostID).Count();
        }



    }
}