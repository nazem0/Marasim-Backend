using Models;

namespace Repository
{
    public class VendorManager : MainManager<Vendor>
    {
        public VendorManager(EntitiesContext _dBContext) : base(_dBContext) { }

    }
}
