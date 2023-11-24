using System;
using System.Collections.Generic;

namespace Models
{
    public class Withdrawal : BaseModel
    {
        public int VendorId { get; set; }
        public string InstaPay { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<Payment> Payments { set; get; }
        public virtual Vendor Vendor { get; set; }
    }
}

