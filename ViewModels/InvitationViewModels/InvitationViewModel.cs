namespace ViewModels.InvitationViewModel
{
    public class InvitationViewModel
    {
        public required string UserId { get; set; }
        public required string GroomName { get; set; }
        public required string GroomPicUrl { get; set; }
        public required string BrideName { get; set; }
        public required string BridePicUrl { get; set; }
        public required DateTime Date { get; set; }
        public required string PosterUrl { get; set; }
        public required string Location { get; set; }
    }
}
