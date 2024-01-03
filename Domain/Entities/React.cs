namespace Domain.Entities
{
    public class React : BaseModel
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }


}