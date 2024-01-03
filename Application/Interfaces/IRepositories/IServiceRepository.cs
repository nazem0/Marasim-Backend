using Application.DTOs.ServiceDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IServiceRepository
    {
        //public IEnumerable<ShowAllServicesViewModel> GetAll();
        public VendorServiceDTO? GetById(int id);
        public HttpStatusCode Add(CreateServiceDTO createServiceDTO, string loggedInUserId);
        public HttpStatusCode Delete(int id, int vendorId);
        //public IEnumerable<ShowAllServicesViewModel> GetActive();
        public IEnumerable<VendorServiceDTO> GetAllVendorServices(string UserId);
        public HttpStatusCode Update(UpdateServiceDTO updateServiceDTO, int vendorId);
    }
}
