using Models;

namespace ViewModels.InvitationViewModel
{
    public static class InvitationExtensions
    {
        public static Invitation ToInvitation(this AddInvitationViewModel Data, string UserId)
        {
            return new Invitation
            {
                UserId = UserId,
                BrideName = Data.BrideName,
                BridePicUrl = Data.BridePicUrl,
                Date = Data.DateTime,
                Location = Data.Location,
                GroomName = Data.GroomName,
                GroomPicUrl = Data.GroomPicUrl,
                PosterUrl = Data.PosterUrl,
            };
        }
        public static InvitationViewModel ToInvitationViewModel(this Invitation Data)
        {
            return new InvitationViewModel
            {
                UserId = Data.UserId,
                BrideName = Data.BrideName,
                BridePicUrl = Data.BridePicUrl,
                Date = Data.Date,
                GroomName = Data.GroomName,
                GroomPicUrl = Data.GroomPicUrl,
                PosterUrl = Data.PosterUrl,
                Location = Data.Location
            };
        }
    }
}
