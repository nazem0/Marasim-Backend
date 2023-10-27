using System.Collections.Generic;

namespace Models
{
    public class Service : BaseModel
    {
        public int VendorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<BookingDetails> BookingDetails { set; get; }

        public virtual ICollection<Review> Reviews { set; get; }
        public virtual ICollection<ServiceAttachment> ServiceAttachments { set; get; }

        public virtual ICollection<PromoCode> PromoCodes { set; get; }

        //public virtual Vendor Vendor { get; set; }
        //public virtual Category Category { get; set; }



    }


}