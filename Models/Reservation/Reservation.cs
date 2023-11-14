using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Reservation : BaseModel
    {
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public float Price { get; set; }
        public char Status { get; set; }
        public string Street { get; set; }= string.Empty;
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        public string District { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Review Review { get; set; }
        public virtual City City { get; set; }
        public virtual Governorate Governorate { get; set; }
    }
}
