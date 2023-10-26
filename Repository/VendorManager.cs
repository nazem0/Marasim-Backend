using Microsoft.AspNetCore.Identity;
using Models;

namespace Repository
{
    public class VendorManager : MainManager<Vendor>
    {
        public VendorManager(EntitiesContext _dBContext) : base(_dBContext)
        {
        }

        public Vendor GetVendorByUserID(string ID)
        {
            return Get().Where(v => v.UserID == ID).FirstOrDefault()!;
        }
    }
}
