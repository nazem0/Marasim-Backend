using System.Collections.Generic;

namespace Models
{
    public class City : BaseModel
    {
        public int GovernorateId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        // Navigation property
        public virtual Governorate Governorate { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
