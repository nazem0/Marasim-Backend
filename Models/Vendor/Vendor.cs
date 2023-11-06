using System.Collections.Generic;

namespace Models
{
    public class Vendor : BaseModel
    {
        public string UserId { get; set; }
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public int CategoryId { get; set; }
        public string ExternalUrl { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Follow> Followers { set; get; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual User User { get; set; }

    }
}
