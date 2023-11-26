using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ServiceAttachmentViewModels
{
    public class AddServiceAttachmentDTO
    {
        public required int ServiceId { get; set; }
        public required IFormFileCollection Attachments { get; set; }
    }
}
