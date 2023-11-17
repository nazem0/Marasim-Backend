using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Linq.Expressions;
using ViewModels.GenerationViewModels;
using ViewModels.PaginationViewModels;
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
        public Vendor? GetVendorByUserId(string Id)
        {
            Vendor? Vendor = EntitiesContext.Vendors.Where(v => v.UserId == Id).FirstOrDefault();
            if (Vendor is null) return null;
            return Vendor;

        }
        public int? GetVendorIdByUserId(string Id)
        {
            Vendor? Vendor = EntitiesContext.Vendors.Where(v => v.UserId == Id).FirstOrDefault();
            return Vendor?.Id;
        }

        public Vendor? GetVendorById(int Id)
        {
            return EntitiesContext.Vendors.Where(v => v.Id == Id).FirstOrDefault();
        }

        public string? GetUserIdByVendorId(int Id)
        {
            return Get(Id)?.UserId;
        }

        public EntityEntry<Vendor> Update(Vendor Entity)
        {
            return EntitiesContext.Update(Entity);
        }

        public IQueryable<VendorMidInfoViewModel> GetIntOfVendors(int NumOfVen = 3)
        {
            if (NumOfVen > EntitiesContext.Vendors.Count())
            {
                NumOfVen = EntitiesContext.Vendors.Count();
            };

            var Data = EntitiesContext.Vendors.OrderByDescending(v => v.Id);

            //.Where(v => v.Services.Average(s => s.Reviews.Any() ? s.Reviews.Average(r => r.Rate) : 0) >= 5);

            return Data.Take(NumOfVen).Select(v => v.ToVendorMidInfoViewModel());
        }

        public async Task<VendorMidInfoViewModel?> GenerateVendorAsync(GenerateVendorViewModel Data)
        {
            IQueryable<Vendor> Vendors = EntitiesContext.Vendors.Where(v => v.CategoryId == Data.CategoryId);

            if (Data.GovernorateId is not null)
                Vendors = Vendors.Where(v => v.GovernorateId == Data.GovernorateId);

            if (Data.CityId is not null)
                Vendors = Vendors.Where(v => v.CityId == Data.CityId);

            var filteredVendors = await Vendors.ToListAsync(); // Materialize the query to avoid subqueries in the average calculations

            if (Data.Rate is not null)
                filteredVendors = filteredVendors
                    .Where(v => v.Services.Average(s => s.Reviews.Any() ? s.Reviews.Average(r => r.Rate) : 0) >= Data.Rate)
                    .ToList();

            if (Data.Price is not null)
                filteredVendors = filteredVendors
                    .Where(v => v.Services.Average(s => s.Price) <= Data.Price)
                    .ToList();

            return filteredVendors
                .OrderBy(v => CalculateAverageRatingOrPrice(v))
                .Select(v => v.ToVendorMidInfoViewModel())
                .FirstOrDefault();
        }

        private double CalculateAverageRatingOrPrice(Vendor vendor)
        {
            IEnumerable<Service>? Services = vendor.Services;
            if (Services.Any())
            {
                double averageRating = 0;
                if (Services.Average(s => s.Reservations.Select(r => r.Review).Count()) > 0)
                {
                    averageRating = vendor.Services
                       .SelectMany(s => s.Reviews)
                       .Average(r => r.Rate);
                }

                double averagePrice = vendor.Services
                    .Average(s => s.Price);

                return Math.Max(averageRating, averagePrice);
            }
            else
            {
                return 0; // You may want to handle this case differently based on your requirements
            }
        }


        public async Task<IEnumerable<VendorMidInfoViewModel>> GeneratePackage(GeneratePackageViewModel Data)
        {
            List<VendorMidInfoViewModel> Package = new();
            foreach (int CategoryId in Data.Categories)
            {
                VendorMidInfoViewModel? Vendor = await GenerateVendorAsync(new GenerateVendorViewModel
                {
                    CategoryId = CategoryId,
                    CityId = Data.CityId,
                    GovernorateId = Data.GovId,
                    Price = Data.Budget / Data.Categories.Length,
                    Rate = Data.Rate
                });
                if (Vendor is not null)
                    Package.Add(Vendor);
            }
            return Package;
        }
        public PaginationViewModel<VendorMinInfoViewModel> Filter(VendorFilterDTO Filter, int PageIndex, int PageSize = 2)
        {
            #region Creating Filter List And It's Elements
            List<Expression<Func<Vendor, bool>>> Filters = new();

            if(Filter.CategoryId is not null)
                Filters.Add(v=>v.CategoryId==Filter.CategoryId);

            if(Filter.CityId is not null)
                Filters.Add(v => v.CityId == Filter.CityId);
            
            if (Filter.GovernorateId is not null)
                Filters.Add(v => v.GovernorateId == Filter.GovernorateId);

            if (Filter.Name is not null)
                Filters.Add(v => v.User.Name.Contains(Filter.Name));

            if (Filter.District is not null)
                Filters.Add(v => v.District.Contains(Filter.District));

            #endregion
            PaginationDTO<Vendor, VendorMinInfoViewModel> PaginationDTO = new()
            {
                Filter = Filters,
                Selector = v => v.ToVendorMinInfoViewModel(),
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get().ToPaginationViewModel(PaginationDTO);
        }
    }
}
