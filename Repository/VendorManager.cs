using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Linq.Expressions;
using ViewModels.CategoryViewModels;
using ViewModels.GenerationViewModels;
using ViewModels.PaginationViewModels;
using ViewModels.VendorViewModels;

namespace Repository
{
    public class VendorManager : MainManager<Vendor>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly CategoryManager CategoryManager;
        public VendorManager(EntitiesContext _dBContext, CategoryManager _categoryManager) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
            CategoryManager = _categoryManager;
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

        public PaginationViewModel<VendorMidInfoViewModel> GetAll(int PageSize, int PageIndex)
        {
            PaginationDTO<VendorMidInfoViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
            };
            return Get()
                .OrderByDescending(v => v.Id)
                .Select(v => v.ToVendorMidInfoViewModel())
                .ToPaginationViewModel(PaginationDTO);
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

        public int Update(UpdateVendorProfileViewModel Data, string UserId)
        {
            Vendor? Vendor = GetVendorByUserId(UserId);
            if (Vendor is null)
                return -1;
            if (Data.Picture is not null)
            {
                Helper.DeleteMediaAsync(Vendor.UserId, "ProfilePicture", Vendor.User.PicUrl);
                FileInfo fi = new(Data.Picture.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Vendor.User.Name = Data.Name ?? Vendor.User.Name;
                Helper.UploadMediaAsync(Vendor.UserId, "ProfilePicture", FileName, Data.Picture);
                Data.PicURL = FileName;
            }

            Vendor.User.Name = Data.Name ?? Vendor.User.Name;
            Vendor.User.PicUrl = Data.PicURL ?? Vendor.User.PicUrl;
            Vendor.User.PhoneNumber = Data.PhoneNumber ?? Vendor.User.PhoneNumber;
            Vendor.Summary = Data.Summary ?? Vendor.Summary;
            Vendor.CategoryId = Data.CategoryId ?? Vendor.CategoryId;
            EntitiesContext.Update(Vendor);
            Save();

            return 1;
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

        public async Task<GeneratedVendorViewModel?> GenerateVendor(GenerateVendorViewModel Data)
        {
            IQueryable<Vendor> Vendors =
                EntitiesContext
                .Vendors
                .Where
                (v => v.CategoryId == Data.CategoryId &&
                v.GovernorateId == Data.GovernorateId &&
                v.Services.Any() &&
                v.Services.Min(s => s.Price) <= Data.Price);
            if (Data.CityId is not null)
                Vendors = Vendors.Where(v => v.CityId == Data.CityId);
            var VendorsList = await Vendors.ToListAsync();
            var Vendor = VendorsList
                .OrderByDescending(v => CalculateAverageRating(v))
                .Select(v => v.ToGeneratedVendorViewModel())
                .FirstOrDefault();

            if (Vendor is null)
            {
                return new GeneratedVendorViewModel
                {
                    Category = CategoryManager.Get(Data.CategoryId)?.ToCategoryNameViewModel().Name,
                };
            }
            else
                return Vendor;
        }

        private double CalculateAverageRating(Vendor vendor)
        {
            IEnumerable<Service>? Services = vendor.Services;
            if (Services.Any() && Services.Average(s => s.Reservations.Select(r => r.Review).Count()) > 0)
            {
                double averageRating = vendor.Services
                   .SelectMany(s => s.Reviews)
                   .Average(r => r.Rate);

                return averageRating;
            }
            else
            {
                return 0; // You may want to handle this case differently based on your requirements
            }
        }


        public async Task<IEnumerable<GeneratedVendorViewModel?>> GeneratePackageAsync(GeneratePackageViewModel Data)
        {
            List<GeneratedVendorViewModel?> Package = new();
            foreach (CategoryPrice CategoryPrice in Data.CategoryPrice)
            {
                GeneratedVendorViewModel? Vendor = await GenerateVendor(new GenerateVendorViewModel
                {
                    CategoryId = CategoryPrice.CategoryId,
                    CityId = Data.CityId,
                    GovernorateId = Data.GovId,
                    Price = CategoryPrice.Price,
                    Rate = Data.Rate
                });
                Package.Add(Vendor);
            }
            return Package;
        }
        public async Task<PaginationViewModel<VendorMidInfoViewModel>> FilterAsync(VendorFilterDTO Filters, int PageIndex, int PageSize = 5)
        {
            PaginationDTO<VendorMidInfoViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            var Data = await Get().Where(Filter(Filters)).ToListAsync();
            return Data.Select(v => v.ToVendorMidInfoViewModel()).ToPaginationViewModel(PaginationDTO);
        }
        private Expression<Func<Vendor, bool>> Filter(VendorFilterDTO Filter)
        {
            return v =>
            (Filter.Name != null ? (Filter.Name.Contains(v.User.Name) || v.User.Name.Contains(Filter.Name)) : true)
            &&
            (Filter.Categories != null ? Filter.Categories.Contains(v.CategoryId.ToString()) : true)
            &&
            (Filter.CityId != null ? Filter.CityId == v.CityId : true)
            &&
            (Filter.GovernorateId != null ? Filter.GovernorateId == v.GovernorateId : true)
            &&
            (Filter.District != null ? (Filter.District.Contains(v.District) || v.District.Contains(Filter.District)) : true)
            &&
            ((Filter.Rate != null) ?
                ((v.Services.Any() && v.Services.Any(s => s.Reviews.Any()) ? Math.Ceiling(v.Services.SelectMany(s => s.Reviews).Average(r => r.Rate)) >= Filter.Rate : false))
            : true)
            ;
        }
    }
}
