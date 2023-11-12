using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.UserViewModels
{
    public class UserMinInfoViewModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
    }
}
