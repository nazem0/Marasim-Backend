using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.VendorViewModels;

namespace Repository
{
    public class VendorManager : MainManager<Vendor>
    {
        private readonly EntitiesContext EntitiesContext;
        public VendorManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Vendor> Add(Vendor Vendor)
        {
            return EntitiesContext.Add(Vendor);
        }
        public Vendor GetVendorByUserId(string Id)
        {
            return Get().Where(v => v.UserId == Id).FirstOrDefault()!;
        }
        public int GetVendorIdByUserId(string Id)
        {
            Vendor Vendor = Get().Where(v => v.UserId == Id).FirstOrDefault()!;
            return Vendor.Id;
        }

        public Vendor GetVendorById(int Id)
        {
            return Get().Where(v => v.Id == Id).FirstOrDefault()!;
        }

        public EntityEntry<Vendor> Update(Vendor Entity)
        {
            return EntitiesContext.Update(Entity);
        }
    }
}
