namespace Models
{
    public class ServiceAttachment : BaseModel
    {
        public int ServiceID { get; set; }
        public string Resource { get; set; }
        public virtual Service Service { get; set; }

    }
}
