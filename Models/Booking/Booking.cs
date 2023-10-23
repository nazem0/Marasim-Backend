﻿using System;
using System.Collections.Generic;

namespace Models
{
    public class Booking : BaseModel
    {
        public string UserID { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<BookingDetails> BookingsDetails { get; set; }
        public virtual User User { get; set; }
    }

}
