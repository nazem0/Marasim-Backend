using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.InvitationViewModel;

namespace Repository
{
    public class InvitationManager : MainManager<Invitation>
    {
        private EntitiesContext EntitiesContext;
        public InvitationManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }

        public bool Add(AddInvitationViewModel Data, string UserId)
        {
            FileInfo fiGroom = new(Data.GroomPic.FileName);
            FileInfo fiBride = new(Data.BridePic.FileName);
            FileInfo fiPoster = new(Data.Poster.FileName);
            string FileNameGroom = DateTime.Now.Ticks + fiGroom.Extension;
            string FileNameBride = DateTime.Now.Ticks + fiBride.Extension;
            string FileNamePoster = DateTime.Now.Ticks + fiPoster.Extension;
            Helper.UploadMediaAsync(UserId, "Invitation", FileNameGroom, Data.GroomPic);
            Helper.UploadMediaAsync(UserId, "Invitation", FileNameBride, Data.BridePic);
            Helper.UploadMediaAsync(UserId, "Invitation", FileNamePoster, Data.Poster);
            Data.GroomPicUrl = FileNameGroom;
            Data.BridePicUrl = FileNameBride;
            Data.PosterUrl = FileNamePoster;
            EntityEntry<Invitation> Entry = EntitiesContext.Add(Data.ToInvitation(UserId));
            if (Entry.State != EntityState.Added)
                return false;
            else
            {
                EntitiesContext.SaveChanges();
                return true;
            }
        }
    }
}
