using Application.DTOs.PaginationDTOs;
using Application.DTOs.VendorDTOs;
using Application.DTOs.VendorGenerationDTOs;
using System.Net;
using ViewModels.VendorViewModels;

namespace Application.Interfaces.IRepositories
{
    public interface IVendorRepository
    {
        public VendorMidInfoDTO? GetVendorMidInfoByUserId(string Id);
        public PaginationDTO<VendorMidInfoDTO> GetAll(int PageIndex, int PageSize);
        public int GetVendorIdByUserId(string Id);
        public VendorDTO? GetVendorById(int Id);
        public string GetUserIdByVendorId(int Id);
        public HttpStatusCode Update(UpdateVendorDTO Data, string UserId);
        public IEnumerable<VendorMidInfoDTO> GetIntOfVendors(int NumOfVen = 3);
        public Task<GeneratedVendorDTO?> GenerateVendor(GenerateVendorDTO Data);
        public Task<IEnumerable<GeneratedVendorDTO?>> GeneratePackageAsync(GeneratePackageDTO Data);
        public Task<PaginationDTO<VendorMidInfoDTO>> FilterAsync(VendorFilterCriteria Filters, int PageIndex, int PageSize = 5);
        public int Count();
        public Task<IEnumerable<VendorMidInfoDTO>> GetVendorsMidInfoAsync();
        public VendorPageDTO? GetVendorFullInfoByUserId(int vendorId);

    }
}
