using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.GenerationViewModels;
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


        public VendorMidInfoViewModel? GenerateVendor
            (GenerateVendorViewModel Data)
        {
            IQueryable<Vendor> Vendors = EntitiesContext.Vendors.Where(v => v.CategoryId == Data.CategoryId);

            if (Data.GovernorateId is not null)
                Vendors = Vendors.Where(v => v.GovernorateId == Data.GovernorateId);

            if (Data.CityId is not null)
                Vendors = Vendors.Where(v => v.CityId == Data.CityId);

            if (Data.Rate is not null)
                Vendors = Vendors.Where(v => v.Services.
                        Average(s => s.Reviews.Any() ?
                        s.Reviews.Average(r => r.Rate) : 0) >= Data.Rate);

            if (Data.Price is not null)
                Vendors = Vendors
                        .Where(v => v.Services
                        .Average(s => s.Price) <= Data.Price);

            return Vendors.Select(v => v.ToVendorMidInfoViewModel()).FirstOrDefault();
        }

        public IEnumerable<VendorMidInfoViewModel> GeneratePackage(GeneratePackageViewModel Data)
        {
            List<VendorMidInfoViewModel> Package = new();
            foreach (int CategoryId in Data.Categories)
            {
                VendorMidInfoViewModel? Vendor = GenerateVendor(new GenerateVendorViewModel
                {
                    CategoryId = CategoryId,
                    CityId = Data.CityId,
                    GovernorateId = Data.GovernorateId,
                    Price = Data.Budget / Data.Categories.Length,
                    Rate = Data.Rate
                });
                if(Vendor is not null)
                    Package.Add(Vendor);
            }
            return Package;
        }
    }
}
