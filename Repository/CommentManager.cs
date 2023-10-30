using Models;

namespace Repository
{
    public class CommentManager : MainManager<Comment>
    {
        public CommentManager(EntitiesContext _dBContext) : base(_dBContext) { }
    }
}

