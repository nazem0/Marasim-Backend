using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.CommentViewModels;
using ViewModels.PaginationViewModels;

namespace Repository
{
    public class CommentManager : MainManager<Comment>
    {
        private readonly EntitiesContext EntitiesContext;
        public CommentManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Comment> Add(Comment comment)
        {
            return EntitiesContext.Add(comment);
        }
        public PaginationViewModel<CommentViewModel> GetByPostId(int PostId, int PageIndex, int PageSize)
        {
            PaginationDTO<CommentViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get().Where(c => c.PostId == PostId).Select(c => c.ToViewModel()).ToPaginationViewModel(PaginationDTO);
        }
        public int GetCommentsCount(int PostId)
        {
            return Get().Where(c => c.PostId == PostId).Count();
        }
    }
}

