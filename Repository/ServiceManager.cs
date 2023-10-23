using Models;


namespace Repository
{
    public class ServiceManager : MainManager<Service>
    {
        public ServiceManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
