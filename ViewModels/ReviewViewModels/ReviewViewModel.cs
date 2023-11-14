using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.UserViewModels;

namespace ViewModels.ReviewViewModels
{
    public class ReviewViewModel
    {
        public int Rate { get; set; }
        public required string Message { get; set; }
        public DateTime DateTime { get; set; }
        public required UserMinInfoViewModel User { get; set; }
    }
}
