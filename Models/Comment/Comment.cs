using System;

namespace Models
{
    public class Comment : BaseModel
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }


}