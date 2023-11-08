using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
