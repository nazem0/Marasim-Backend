using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PostAttachmentsViewModel;
using ViewModels.ServiceAttachmentViewModels;

namespace Repository
{
    public class PostAttachmentManager : MainManager<PostAttachment>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly PostManager PostManager;

        public PostAttachmentManager(PostManager _PostManager, EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
            PostManager = _PostManager;

        }
        public EntityEntry<PostAttachment> Add(PostAttachment Entity)
        {
            return EntitiesContext.Add(Entity);
        }

        public IEnumerable<PostAttachmentCustomViewModel> GetByPostId(int PostId)
        {
            return Get().Where(pa => pa.PostId == PostId).Select(pa => pa.ToCustomModel());
        }

        public bool Add(AddPostAttachmentsDTO Data, int VendorId)
        {
            Post? Post = PostManager.Get(Data.PostId);
            if (Post is null) return false;
            if (Post.VendorId != VendorId) return false;
            List<EntityState> AdditionStates = new();
            foreach (IFormFile item in Data.Attachments)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (Post.Vendor.UserId, "PostAttachment", FileName, item, $"{Data.PostId}-{Post.VendorId}");
                EntityEntry Addition = EntitiesContext.Add(
                    new PostAttachment
                    {
                        AttachmentUrl = FileName,
                        PostId = Data.PostId
                    }
                    );
                if (Addition.State is EntityState.Added)
                    AdditionStates.Add(Addition.State);
            }
            if (AdditionStates.Count != Data.Attachments.Count)
                return false;
            else
            {
                Save();
                return true;
            }
        }
        public bool Delete(int AttachmentId, int VendorId)
        {
            PostAttachment? PA = Get(AttachmentId);
            if (PA is null) return false;
            if (PA.Post.VendorId != VendorId) return false;
            if (!(PostManager.Get(PA.PostId)?.PostAttachments.Count > 1)) return false;
            Delete(PA);
            Save();
            return true;
        }
    }
}

