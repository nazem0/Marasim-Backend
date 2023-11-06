﻿using System;


namespace Models
{
    public class Follow : BaseModel
    {
        public string UserId { get; set; }
        public int VendorId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Vendor Vendor { get; set; }
    }

}
