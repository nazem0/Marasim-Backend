using Models;

namespace Repository
{
    public class FollowManager : MainManager<Follow>
    {
        public FollowManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
