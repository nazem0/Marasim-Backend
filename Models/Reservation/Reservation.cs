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
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
