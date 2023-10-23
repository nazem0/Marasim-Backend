﻿using System;

namespace Models
{
    public class BookingDetails
    {
        public int BookingID { get; set; }
        public int ServiceID { get; set; }
        public string Code { get; set; }
        public Status Status { get; set; }
        public float Payment { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Service Service { get; set; }
    }


}