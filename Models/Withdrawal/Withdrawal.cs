using System;
using System.Collections.Generic;

namespace Models
{
    public class Withdrawal : BaseModel
    {
        public int VendorId;
        public string InstaPay;
        public DateTime DateTime;
        public bool IsConfirmed;
        public virtual ICollection<Payment> Payments { set; get; }
        public virtual Vendor Vendor { get; set; }
    }
}

