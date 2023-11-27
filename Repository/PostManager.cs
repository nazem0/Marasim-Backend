using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.PostAttachmentsViewModel;
using ViewModels.PostViewModels;

namespace Repository
{
    public class PostManager : MainManager<Post>
    {
        private readonly EntitiesContext EntitiesContext;
        public PostManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Post> Add(Post Entity)
        {
            return EntitiesContext.Add(Entity);
        }

        public PaginationViewModel<PostViewModel> GetByVendorId(int VendorId, int PageSize, int PageIndex)
        {
            PaginationDTO<PostViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Where(p => p.Vendor.Id == VendorId)
                .Select(r => r.ToViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }

        public PaginationViewModel<PostViewModel> GetByPostsByFollow(string UserId, int PageSize, int PageIndex)
        {
            PaginationDTO<PostViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Where(p => p.Vendor.Followers.Any(f => f.UserId == UserId))
                .Select(r => r.ToViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }

        public EntityEntry<Post> Update(Post Entity)
        {
            return EntitiesContext.Update(Entity);
        }

        public void Delete(int PostId)
        {
            Post? Post = Get(PostId);
            if (Post != null)
            {
                base.Delete(Post);
            }
            else
            {
                throw new Exception("Post Is Not Found");
            }
        }

        //Attachment Actions
        public IEnumerable<PostAttachmentViewModel>? GetAttachments(int PostId)
        {
            Post? Post = Get(PostId);
            if (Post is null) return null;
            return Post.PostAttachments.Select(pa => pa.ToViewModel());
        }
        public int AddAttachments(AddPostAttachmentsDTO Data, string UserId)
        {
            Post? Post = Get(Data.PostId);
            if (Post is null) return 404;
            if (Post.Vendor.UserId != UserId) return 401;
            foreach (IFormFile item in Post.PostAttachments)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (Post.Vendor.UserId, "PostAttachment", FileName, item, $"{Data.PostId}-{Post.VendorId}");
                Post.PostAttachments.Add(new PostAttachment
                {
                    AttachmentUrl = fi.Name,
                    Post = Post
                });
            }
            EntitiesContext.Update(Post);
            Save();
            return 200;
        }
        public int DeleteAttachment(int AttachmentId,string UserId)
        {
            PostAttachment? PA = EntitiesContext.PostsAttachments.Where(pa => pa.Id == AttachmentId).FirstOrDefault();
            if(PA is null) return 404;
            if (PA.Post.Vendor.UserId != UserId) return 401;
            PA.Post.PostAttachments.Remove(PA);
            Save();
            return 200;
        }
    }
}

