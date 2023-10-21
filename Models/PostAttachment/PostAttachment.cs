namespace Models
{
    public class PostAttachment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string AttachmentUrl { get; set; }
        public virtual Post Post { get; set; }
    }


}