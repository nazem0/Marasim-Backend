using Models;

namespace Repository
{


    public class ServiceAttachmentManager : MainManager<ServiceAttachment>
    {
        public ServiceAttachmentManager(EntitiesContext _dBContext) : base(_dBContext) { }
    }
}
