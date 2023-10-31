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
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int VendorID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public int ReviewsCount { get; set; }
        public bool IsDeleted { get; set; }
        public virtual required ICollection<ServiceAttachment> ServiceAttachments { set; get; }
        public virtual required PromoCode PromoCode { set; get; }
        public virtual required ICollection<BookingDetails> BookingDetails { set; get; }


    }
}
