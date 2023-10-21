using System;

namespace Models
{
    public class React
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int PostID { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }


}