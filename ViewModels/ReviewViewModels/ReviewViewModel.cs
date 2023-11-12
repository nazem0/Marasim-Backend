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
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public UserMinInfoViewModel User { get; set; }
    }
}
