using Application.DTOs.ServiceAttachmentsDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class ServiceAttachmentRepository : IServiceAttachmentRepository
    {
        private readonly DbSet<ServiceAttachment> _serviceAttachments;
        private readonly DbSet<Service> _services;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceAttachmentRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _services = entitiesContext.Services;
            _serviceAttachments = entitiesContext.ServiceAttachments;
        }
        public HttpStatusCode Add(CreateServiceAttachmentDTO Data, int VendorId)
        {
            Service? Service = _services.Where(s => s.Id == Data.ServiceId && s.VendorId == VendorId).FirstOrDefault();
            if (Service is null) return HttpStatusCode.NotFound;
            foreach (IFormFile item in Data.Attachments)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (Service.Vendor.UserId
                    , "ServiceAttachment", FileName, item, $"{Data.ServiceId}-{Service.VendorId}");
                _serviceAttachments.Add(
                    new ServiceAttachment
                    {
                        AttachmentUrl = FileName,
                        ServiceId = Data.ServiceId
                    }
                    );
            }
            return _unitOfWork.SaveChanges();
        }
        public IEnumerable<IndependentServiceAttachmentDTO> GetByServiceId(int ServiceId)
        {
            return _serviceAttachments.Where(sa => sa.ServiceId == ServiceId).Select(sa => sa.ToIndependentServiceAttachmentDTO());
        }
        public IEnumerable<IndependentServiceAttachmentDTO> GetByVendorId(int VendorId)
        {
            return _serviceAttachments.Where(sa => sa.Service.VendorId == VendorId && sa.Service.IsDeleted == false)
                .Select(sa => sa.ToIndependentServiceAttachmentDTO());
        }

        public IEnumerable<ServiceAttachmentDTO> GetAllActive()
        {
            return _serviceAttachments.Where(sa => sa.Service.IsDeleted == false)
                .Select(sa => sa.ToServiceAttachmentDTO());
        }

        public IEnumerable<IndependentServiceAttachmentDTO> GetCustomAttachment()
        {
            return _serviceAttachments.Where(sa => sa.Service.IsDeleted == false)
                .Select(sa => sa.ToIndependentServiceAttachmentDTO());
        }
        public HttpStatusCode Delete(int serviceAttachmentId, int vendorId)
        {
            ServiceAttachment? serviceAttachment = _serviceAttachments.Where(s => s.Id == serviceAttachmentId && s.Service.VendorId == vendorId).FirstOrDefault();
            if (serviceAttachment == null) return HttpStatusCode.NotFound;
            _serviceAttachments.Remove(serviceAttachment);
            return _unitOfWork.SaveChanges();
        }
    }
}
