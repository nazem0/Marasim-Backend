namespace Models
{
    public class PostAttachment : BaseModel
    {
        public int PostID { get; set; }
        public string AttachmentUrl { get; set; }
        public virtual Post Post { get; set; }
    }


}