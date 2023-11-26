using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ServiceAttachmentViewModels;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ServiceAttachmentManager : MainManager<ServiceAttachment>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly ServiceManager ServiceManager;
        public ServiceAttachmentManager(ServiceManager _serviceManager, EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
            ServiceManager = _serviceManager;
        }
        public bool Add(AddServiceAttachmentDTO Data,int VendorId)
        {
            Service? Service = ServiceManager.Get(Data.ServiceId);
            if(Service is null) return false;
            if(Service.VendorId !=  VendorId) return false;
            List<EntityState> AdditionStates = new();
            foreach (IFormFile item in Data.Attachments)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (Service.Vendor.UserId
                    , "ServiceAttachment", FileName, item, $"{Data.ServiceId}-{Service.VendorId}");
                EntityEntry Addition = EntitiesContext.Add(
                    new ServiceAttachment
                    {
                        AttachmentUrl = FileName,
                        ServiceId = Data.ServiceId
                    }
                    );
                if (Addition.State is EntityState.Added)
                    AdditionStates.Add(Addition.State);
            }
            if (AdditionStates.Count != Data.Attachments.Count)
                return false;
            else
            {
                Save();
                return true;
            }
        }
        public IEnumerable<ServiceAttachmentCustomViewModel> GetByServiceId(int ServiceId)
        {
            return Get().Where(sa => sa.ServiceId == ServiceId).Select(sa => sa.ToCustomViewModel());
        }
        public IQueryable<ServiceAttachmentCustomViewModel> GetByVendorId(int VendorId)
        {
            return Get().Where(sa => sa.Service.VendorId == VendorId && sa.Service.IsDeleted == false)
                .Select(sa => sa.ToCustomViewModel());
        }

        public IQueryable<ServiceAttachmentViewModel> GetAllActive()
        {
            return Get().Where(sa => sa.Service.IsDeleted == false)
                .Select(sa => sa.ToViewModel());
        }

        public IQueryable<ServiceAttachmentCustomViewModel> GetCustomAttachment()
        {
            return Get().Where(sa => sa.Service.IsDeleted == false)
                .Select(sa => sa.ToCustomViewModel());
        }

        public bool Delete(int Id, int VendorId)
        {
            ServiceAttachment? SA = Get(Id);
            if (SA is null) return false;
            if (SA.Service.VendorId != VendorId)
                return false;
            Delete(SA);
            Save();
            return true;
        }
    }
}
