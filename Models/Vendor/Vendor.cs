using System.Collections.Generic;

namespace Models
{
    public class Vendor
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Follow> Followers { set; get; }
        public virtual ICollection<Service> Services { get; set; }

        public virtual User User { get; set; }

    }
}
