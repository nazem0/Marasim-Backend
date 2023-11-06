using Models;

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

        public Vendor GetVendorByID(int ID)
        {
            return Get().Where(v => v.ID == ID).FirstOrDefault()!;
        }
    }
}
