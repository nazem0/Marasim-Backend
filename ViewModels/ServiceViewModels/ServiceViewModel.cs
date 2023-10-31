using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ServiceAttatchmentViewModels;
namespace ViewModels.ServiceViewModels
{
    public class ServiceViewModel
    {
        public int VendorID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }
        public virtual required IEnumerable<ServiceAttatchmentViewModel> ServiceAttachments { set; get; }
        public virtual required PromoCode PromoCode { set; get; }

    }
}
