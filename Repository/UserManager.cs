using Models;

namespace Repository
{
    public class UserManager : MainManager<User>
    {
        public UserManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
