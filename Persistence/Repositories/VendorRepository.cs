using Application.DTOs.PaginationDTOs;
using Application.DTOs.VendorDTOs;
using Application.DTOs.VendorGenerationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Persistence.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly DbSet<Vendor> _vendors;
        private readonly DbSet<Category> _categories;
        private readonly IUnitOfWork _unitOfWork;
        public VendorRepository(AppDbContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _vendors = entitiesContext.Vendors;
            _categories = entitiesContext.Categories;
            _unitOfWork = unitOfWork;
        }

        public VendorMidInfoDTO? GetVendorMidInfoByUserId(string Id)
        {
            var Vendor = _vendors.Where(v => v.UserId == Id).FirstOrDefault()?.ToVendorMidInfoDTO();
            return Vendor;

        }
        public VendorPageDTO? GetVendorFullInfoByUserId(int vendorId)
        {
            var Vendor = _vendors.Where(v => v.Id == vendorId).FirstOrDefault()?.ToVendorPageDTO();
            return Vendor;

        }
        public async Task<IEnumerable<VendorMidInfoDTO>> GetVendorsMidInfoAsync()
        {
            return await _vendors.Select(v => v.ToVendorMidInfoDTO()).ToListAsync();
        }
        public PaginationDTO<VendorMidInfoDTO> GetAll(int PageIndex, int PageSize)
        {
            return _vendors
                .OrderByDescending(v => v.Id)
                .Select(v => v.ToVendorMidInfoDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        public int GetVendorIdByUserId(string Id)
        {
            Vendor Vendor = _vendors.Where(v => v.UserId == Id).First();
            return Vendor.Id;
        }

        public VendorDTO? GetVendorById(int Id)
        {
            return _vendors.Where(v => v.Id == Id).FirstOrDefault()?.ToVendorDTO();
        }

        public string GetUserIdByVendorId(int Id)
        {
            return _vendors.Find(Id)!.UserId;
        }

        public HttpStatusCode Update(UpdateVendorDTO Data, string UserId)
        {
            Vendor? Vendor = _vendors.Where(v => v.UserId == UserId).FirstOrDefault();
            if (Vendor is null) return HttpStatusCode.NotFound;
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
            _vendors.Update(Vendor);
            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<VendorMidInfoDTO> GetIntOfVendors(int NumOfVen = 3)
        {
            var Data = _vendors.OrderByDescending(v => v.User.RegistrationDate);
            return Data.Take(NumOfVen).Select(v => v.ToVendorMidInfoDTO());
        }

        public async Task<GeneratedVendorDTO?> GenerateVendor(GenerateVendorDTO Data)
        {
            IQueryable<Vendor> Vendors =
                _vendors
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
                Category? category = _categories.Find(Data.CategoryId);
                return new GeneratedVendorDTO
                {
                    Category = category is not null ? category.Name : "",
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


        public async Task<IEnumerable<GeneratedVendorDTO?>> GeneratePackageAsync(GeneratePackageDTO Data)
        {
            List<GeneratedVendorDTO?> Package = new();
            foreach (CategoryPrice CategoryPrice in Data.CategoryPrice)
            {
                GeneratedVendorDTO? Vendor = await GenerateVendor(new GenerateVendorDTO
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
        public async Task<PaginationDTO<VendorMidInfoDTO>> FilterAsync(VendorFilterCriteria Filters, int PageIndex, int PageSize = 5)
        {
            var vendors = await _vendors.Where(Filter(Filters)).Select(v => v.ToVendorMidInfoDTO()).ToListAsync();
            return vendors.ToPaginationDTO(PageIndex, PageSize);
        }
        private Expression<Func<Vendor, bool>> Filter(VendorFilterCriteria Filter)
        {
            return v =>
            (Filter.Name == null || (Filter.Name.Contains(v.User.Name) || v.User.Name.Contains(Filter.Name)))
            &&
            (Filter.Categories == null || Filter.Categories.Contains(v.CategoryId.ToString()))
            &&
            (Filter.CityId == null || Filter.CityId == v.CityId)
            &&
            (Filter.GovernorateId == null || Filter.GovernorateId == v.GovernorateId)
            &&
            (Filter.District == null || (Filter.District.Contains(v.District) || v.District.Contains(Filter.District)))
            &&
            (Filter.Rate == null || ((v.Services.Any() && v.Services.Any(s => s.Reviews.Any()) && Math.Ceiling(v.Services.SelectMany(s => s.Reviews).Average(r => r.Rate)) >= Filter.Rate)));
            #region OldCode
            //return v =>
            //(Filter.Name != null ? (Filter.Name.Contains(v.User.Name) || v.User.Name.Contains(Filter.Name)) : true)
            //&&
            //(Filter.Categories != null ? Filter.Categories.Contains(v.CategoryId.ToString()) : true)
            //&&
            //(Filter.CityId != null ? Filter.CityId == v.CityId : true)
            //&&
            //(Filter.GovernorateId != null ? Filter.GovernorateId == v.GovernorateId : true)
            //&&
            //(Filter.District != null ? (Filter.District.Contains(v.District) || v.District.Contains(Filter.District)) : true)
            //&&
            //((Filter.Rate != null) ?
            //    ((v.Services.Any() && v.Services.Any(s => s.Reviews.Any()) ? Math.Ceiling(v.Services.SelectMany(s => s.Reviews).Average(r => r.Rate)) >= Filter.Rate : false))
            //: true);
            #endregion
        }
        public int Count()
        {
            return _vendors.Count();
        }
    }
}
