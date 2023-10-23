
using System.Collections.Generic;

namespace Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string PicUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Service> Services { set; get; }
    }

}