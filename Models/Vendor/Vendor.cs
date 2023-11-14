using System.Collections.Generic;

namespace Models
{
    public class Vendor : BaseModel
    {
        public string UserId { get; set; }
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? Latitude { get; set; } = null;
        public decimal? Longitude { get; set; } = null;
        public string Street { get; set; } = string.Empty;
        public int CityId { get; set; }
        public int GovernorateId { get; set; }
        public string District { get; set; }
        public int CategoryId { get; set; }
        public string ExternalUrl { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Follow> Followers { set; get; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual Governorate Governorate { get; set; }

    }
}
