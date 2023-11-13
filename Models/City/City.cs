using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class City:BaseModel
    {
        public int GovernorateId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        // Navigation property
        public virtual Governorate Governorate { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
