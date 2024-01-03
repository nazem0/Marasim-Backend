using Application.DTOs.ServiceAttachmentsDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class ServiceAttachmentExtensions
    {

        public static ServiceAttachmentDTO ToServiceAttachmentDTO(this ServiceAttachment serviceAttachment)
        {
            return new ServiceAttachmentDTO
            {
                Id = serviceAttachment.Id,
                ServiceId = serviceAttachment.ServiceId,
                AttachmentUrl = serviceAttachment.AttachmentUrl
            };
        }

        public static IndependentServiceAttachmentDTO ToIndependentServiceAttachmentDTO(this ServiceAttachment serviceAttachment)
        {
            return new IndependentServiceAttachmentDTO
            {
                Id = serviceAttachment.Id,
                ServiceId = serviceAttachment.ServiceId,
                AttachmentUrl = serviceAttachment.AttachmentUrl,
                UserId = serviceAttachment.Service.Vendor.UserId,
                VendorId = serviceAttachment.Service.VendorId
            };
        }
    }
}
