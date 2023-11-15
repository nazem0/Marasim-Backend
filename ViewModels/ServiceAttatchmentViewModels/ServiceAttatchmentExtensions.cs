using Models;

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
