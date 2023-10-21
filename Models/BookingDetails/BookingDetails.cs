using System;

namespace Models
{
    public class BookingDetails
    {
        public virtual int ID { get; set; }
        public virtual int BookingID { get; set; }
        public virtual int ServiceID { get; set; }
        public virtual string Code { get; set; }
        public virtual Status Status { get; set; }
        public virtual float Payment { get; set; }
        public virtual DateTime DateTime { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Service Service { get; set; }
    }


}