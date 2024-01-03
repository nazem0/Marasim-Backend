namespace Domain.Entities
{
    public class Invitation : BaseModel
    {
        public string UserId { get; set; }
        public string GroomName { get; set; }
        public string GroomPicUrl { get; set; }
        public string BrideName { get; set; }
        public string BridePicUrl { get; set; }
        public string PosterUrl { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
    }

}