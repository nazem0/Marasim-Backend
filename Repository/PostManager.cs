using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
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
    }
}

