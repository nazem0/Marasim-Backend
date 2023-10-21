using System.Collections.Generic;

namespace Models
{
    public class Service
    {
        public  int ID { get; set; }
        public int VendorID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Street { get; set; } //Allow Null
        public string District { get; set; }
        public string Governance { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<BookingDetails> BookingDetails { set; get; }

        public virtual ICollection<Review> Reviews { set; get; }
        public virtual ICollection<ServiceAttachment> ServiceAttachments { set; get; }

        public virtual ICollection<PromoCode> PromoCodes { set; get; }

        public virtual Vendor Vendor { get; set; }
        public virtual Category Category { get; set; }



    }


}