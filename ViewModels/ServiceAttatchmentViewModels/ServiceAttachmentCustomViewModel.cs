using System;
using Models;

namespace ViewModels.ServiceAttatchmentViewModels
{
    public class ServiceAttachmentCustomViewModel
    {
        public required int ServiceId { get; set; }
        public required string AttachmentUrl { get; set; }
        public required string UserId { get; set; }
        public required int VendorId { get; set; }
    }
}


