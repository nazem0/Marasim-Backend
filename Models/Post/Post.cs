using System;
using System.Collections.Generic;

namespace Models
{
    public class Post
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<PostAttachment> PostAttachments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<React> Reacts { get; set; }

        public bool IsDeleted { get; set; }
    }


}