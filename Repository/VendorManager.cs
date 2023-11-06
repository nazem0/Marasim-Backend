using Models;
using ViewModels.VendorViewModels;

namespace Repository
{
    public class VendorManager : MainManager<Vendor>
    {
        public VendorManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public Vendor GetVendorByUserId(string ID)
        {
            return Get().Where(v => v.UserID == ID).FirstOrDefault()!;
        }
        public int GetVendorIdByUserId(string ID)
        {
            return Get().Where(v => v.UserID == ID).FirstOrDefault()!.ID;
        }
    }
}
