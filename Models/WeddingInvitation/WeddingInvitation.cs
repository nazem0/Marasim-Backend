using System;

namespace Models
{
    public class WeddingInvitation : BaseModel
    {
        public string UserID { get; set; }
        public string Decsription { get; set; }
        public string GroomName { get; set; }
        public string GroomPicUrl { get; set; }
        public string BrideName { get; set; }
        public string BridePicUrl { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }

}