using System;

namespace Models
{
    public class Payment : BaseModel
    {
        public string InstaPay;
        public DateTime DateTime;
        public int ReservationId;
        public virtual Reservation Reservation { get; set; }
    }
}
