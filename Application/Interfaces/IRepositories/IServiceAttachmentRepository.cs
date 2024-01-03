using Application.DTOs.ServiceAttachmentsDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IServiceAttachmentRepository
    {
        public HttpStatusCode Add(CreateServiceAttachmentDTO Data, int VendorId);
        public IEnumerable<IndependentServiceAttachmentDTO> GetByServiceId(int ServiceId);
        public IEnumerable<IndependentServiceAttachmentDTO> GetByVendorId(int VendorId);
        public IEnumerable<ServiceAttachmentDTO> GetAllActive();
        public IEnumerable<IndependentServiceAttachmentDTO> GetCustomAttachment();
        public HttpStatusCode Delete(int serviceAttachmentId, int vendorId);
    }
}
