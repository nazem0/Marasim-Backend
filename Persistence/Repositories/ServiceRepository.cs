using Application.DTOs.ServiceAttachmentsDTOs;
using Application.DTOs.ServiceDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DbSet<Service> _services;
        private readonly DbSet<Vendor> _vendors;
        private readonly IServiceAttachmentRepository _serviceAttachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceRepository(AppDbContext entitiesContext, IServiceAttachmentRepository serviceAttachmentRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _vendors = entitiesContext.Vendors;
            _services = entitiesContext.Services;
            _serviceAttachmentRepository = serviceAttachmentRepository;
        }
        //Temp Disabled
        //public IEnumerable<ShowAllServicesViewModel> GetAll()
        //{
        //    return Get().Select(s => s.ToShowAllServicesViewModel());
        //}
        public VendorServiceDTO? GetById(int id)
        {
            var service = _services.Find(id);
            return service?.ToVendorServiceDTO();
        }
        /*
        ████████╗███████╗███████╗████████╗
        ╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝
           ██║   █████╗  ███████╗   ██║   
           ██║   ██╔══╝  ╚════██║   ██║   
           ██║   ███████╗███████║   ██║   
           ╚═╝   ╚══════╝╚══════╝   ╚═╝   
        */
        public HttpStatusCode Add(CreateServiceDTO createServiceDTO, string loggedInUserId)
        {
            int vendorId = _vendors.Where(v => v.UserId == loggedInUserId).First().Id;
            Service? service = _services.Add(createServiceDTO.ToEntity(vendorId)).Entity;
            var serviceCreation = _unitOfWork.SaveChanges();
            if (serviceCreation != HttpStatusCode.OK) return serviceCreation;
            _serviceAttachmentRepository.Add(new CreateServiceAttachmentDTO { Attachments = createServiceDTO.Pictures, ServiceId = service.Id }, vendorId);
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Delete(int id, int vendorId)
        {
            Service? Service = _services.Where(s => s.Id == id && s.VendorId == vendorId).FirstOrDefault();
            if (Service is null) return HttpStatusCode.NotFound;
            Service.IsDeleted = true;
            return _unitOfWork.SaveChanges();
        }
        //public IEnumerable<ShowAllServicesViewModel> GetActive()
        //{
        //    return Get().Where(s => s.IsDeleted == false).Select(s => s.ToShowAllServicesViewModel());
        //}

        public IEnumerable<VendorServiceDTO> GetAllVendorServices(string UserId)
        {

            var Data = _services.Where(s => s.Vendor.UserId == UserId && s.IsDeleted == false);
            return Data.Select(s => s.ToVendorServiceDTO());
        }

        public HttpStatusCode Update(UpdateServiceDTO updateServiceDTO, int vendorId)
        {
            Service? service = _services.Where(s => s.Id == updateServiceDTO.Id && s.VendorId == vendorId).FirstOrDefault();
            if (service is null) return HttpStatusCode.NotFound;
            service.Title = updateServiceDTO.Title ?? service.Title;
            service.Description = updateServiceDTO.Description ?? service.Description;
            service.Price = updateServiceDTO.Price ?? service.Price;
            _services.Update(service);
            return _unitOfWork.SaveChanges();
        }

    }
}
