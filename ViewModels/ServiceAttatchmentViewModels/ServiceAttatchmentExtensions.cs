using Models;

namespace ViewModels.ServiceAttachmentViewModels
{
    public static class ServiceAttatchmentExtensions
    {
        //public static ICollection<ServiceAttatchmentViewModel> ToServiceAttatchmentViewModel(this ICollection<ServiceAttachment> Model)
        //{
        //    return new ServiceAttatchmentViewModel { AttachmentUrl = Model. };
        //}

        public static ServiceAttachmentViewModel ToViewModel(this ServiceAttachment ServiceAttachment)
        {
            return new ServiceAttachmentViewModel
            {
                Id = ServiceAttachment.Id,
                ServiceId = ServiceAttachment.ServiceId,
                AttachmentUrl = ServiceAttachment.AttachmentUrl
            };
        }

        public static ServiceAttachmentCustomViewModel ToCustomViewModel(this ServiceAttachment ServiceAttachment)
        {
            return new ServiceAttachmentCustomViewModel
            {
                ServiceId = ServiceAttachment.ServiceId,
                AttachmentUrl = ServiceAttachment.AttachmentUrl,
                UserId = ServiceAttachment.Service.Vendor.UserId,
                VendorId = ServiceAttachment.Service.VendorId
            };
        }
    }
}
