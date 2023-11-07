using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.VendorViewModels
{
    public class VendorMinInfoViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required string UserId;

    }
}
