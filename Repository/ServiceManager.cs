using Models;

namespace Repository
{
    public class ServiceManager : MainManager<Service>
    {
        public ServiceManager(EntitiesContext _dBContext) : base(_dBContext) { }
        public void Delete(int Id)
        {
            Service? Service = Get(Id);
            if (Service != null)
            {
                Service.IsDeleted = true;
            }
            else
            {
                throw new Exception("Service Is Not Found");
            }
        }
        public IQueryable<Service> GetActive()
        {
            return Get().Where(s => s.IsDeleted == false);
        }
    }
}
