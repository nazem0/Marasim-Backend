using Models;

namespace Repository
{
    public class CheckListManager : MainManager<CheckList>
    {
        public CheckListManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
