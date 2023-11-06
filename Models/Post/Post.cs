using System;
using System.Collections.Generic;

namespace Models
{
    public class Post : BaseModel
    {
        public int VendorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int? ServiceID { get; set; } = null;
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PostAttachment> PostAttachments { set; get; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
    }


}