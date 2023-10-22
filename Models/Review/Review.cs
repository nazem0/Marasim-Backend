using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Review
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ServiceID { get; set; }
        public int Rate { get; set; }
        public string Massage { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
    }
}
