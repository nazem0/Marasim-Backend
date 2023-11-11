namespace Models
{
    public class ServiceAttachment : BaseModel
    {
        public int ServiceId { get; set; }
        public string AttachmentUrl { get; set; }
        public virtual Service Service { get; set; }

    }
}
