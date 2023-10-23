using System;


namespace Models
{
    public class Follow : BaseModel
    {
        public string UserID { get; set; }
        public int VendorID { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Vendor Vendor { get; set; }
    }

}
