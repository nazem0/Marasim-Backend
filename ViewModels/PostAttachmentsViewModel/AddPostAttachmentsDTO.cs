using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PostAttachmentsViewModel
{
    public class AddPostAttachmentsDTO
    {
        public required int PostId { get; set; }
        public required IFormFileCollection Attachments { get; set; }
    }
}
