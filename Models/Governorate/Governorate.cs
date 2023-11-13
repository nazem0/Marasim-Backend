using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Governorate:BaseModel
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        // Navigation property
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
