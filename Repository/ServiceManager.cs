using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ServiceViewModels;

namespace Repository
{
    public class ServiceManager : MainManager<Service>
    {
        private readonly EntitiesContext EntitiesContext;
        public ServiceManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public IEnumerable<ShowAllServicesViewModel> GetAll()
        {
            return Get().Select(s => s.ToShowAllServicesViewModel());
        }
        public EntityEntry<Service> Add(Service entity)
        {
            return EntitiesContext.Add(entity);
        }
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
        public IEnumerable<ShowAllServicesViewModel> GetActive()
        {
            return Get().Where(s => s.IsDeleted == false).Select(s => s.ToShowAllServicesViewModel());
        }

        public IEnumerable<ServiceViewModel> GetAllVendorServices(string UserId)
        {

            var Data = Get().Where(s => s.Vendor.UserId == UserId);
            return Data.Select(s => s.ToServiceViewModel());
        }

        public EntityEntry<Service> Update(Service Entity)
        {
            return EntitiesContext.Update(Entity);
        }
    }
}
