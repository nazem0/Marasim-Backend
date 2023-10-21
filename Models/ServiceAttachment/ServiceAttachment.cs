using System.Collections.Generic;
namespace Models
{
    public class ServiceAttachment
    {
        public int ID { get; set; }
        public int ServiceID { get; set; }
        public string AttachmentUrl { get; set; }
        public virtual Service Service { get; set; }

    }
}