using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.PostViewModels;

namespace ViewModels.ServiceAttatchmentViewModels
{
    public static class ServiceAttatchmentExtensions
    {
        //public static ICollection<ServiceAttatchmentViewModel> ToServiceAttatchmentViewModel(this ICollection<ServiceAttachment> Model)
        //{
        //    return new ServiceAttatchmentViewModel { AttachmentUrl = Model. };
        //}

        public static ServiceAttatchmentViewModel ToViewModel(this ServiceAttachment ServiceAttachment)
        {
            return new ServiceAttatchmentViewModel
            {
                Id = ServiceAttachment.Id,
                ServiceID = ServiceAttachment.ServiceID,
                AttachmentUrl = ServiceAttachment.AttachmentUrl
            };
        }

        public static ServiceAttachmentCustomViewModel ToCustomViewModel(this ServiceAttachment ServiceAttachment)
        {
            return new ServiceAttachmentCustomViewModel
            {
                ServiceId = ServiceAttachment.ServiceID,
                AttachmentUrl = ServiceAttachment.AttachmentUrl,
                UserId = ServiceAttachment.Service.Vendor.UserId,
                VendorId = ServiceAttachment.Service.VendorID
            };
        }
    }
}
