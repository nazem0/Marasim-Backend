using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{


    public class ServiceAttachmentManager : MainManager<ServiceAttachment>
    {
    private readonly EntitiesContext EntitiesContext;
        public ServiceAttachmentManager(EntitiesContext _dBContext) : base(_dBContext) {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<ServiceAttachment> Add(ServiceAttachment Entity)
        {
            return EntitiesContext.Add(Entity);
        }
    }
}
