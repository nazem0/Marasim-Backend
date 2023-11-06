using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class ServiceManager : MainManager<Service>
    {
        private readonly EntitiesContext EntitiesContext;
        public ServiceManager(EntitiesContext _dBContext) : base(_dBContext) {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Service> Add(Service entity)
        {
            return EntitiesContext.Add(entity);
        }
        public void Delete(int Id)
        {
            Service? Service = Get(Id).FirstOrDefault();
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

        public EntityEntry<Service> Update(Service Entity)
        {
            return EntitiesContext.Update(Entity);
        }
    }
}
