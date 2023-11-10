﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.ServiceAttatchmentViewModels;

namespace Repository
{
    public class ServiceAttachmentManager : MainManager<ServiceAttachment>
    {
        private readonly EntitiesContext EntitiesContext;
        public ServiceAttachmentManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<ServiceAttachment> Add(ServiceAttachment Entity)
        {
            return EntitiesContext.Add(Entity);
        }

        public IQueryable<ServiceAttatchmentViewModel> GetByVendorId(int VendorId)
        {
            return Get().Where(sa => sa.Service.VendorID == VendorId && sa.Service.IsDeleted == false)
                .Select(sa => sa.ToViewModel());
        }

        public IQueryable<ServiceAttatchmentViewModel> GetAllActive()
        {
            return Get().Where(sa => sa.Service.IsDeleted == false)
                .Select(sa => sa.ToViewModel());
        }
    }
}
