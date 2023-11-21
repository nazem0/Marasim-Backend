using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.ReactViewModels;

namespace Repository
{
    public class ReactManager : MainManager<React>
    {
        private readonly EntitiesContext EntitiesContext;
        public ReactManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<React> Add(React Entity)
        {
            return EntitiesContext.Add(Entity);
        }
        public PaginationViewModel<ReactViewModel> GetByPostId(int PostId, int PageIndex, int PageSize)
        {
            PaginationDTO<ReactViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };

            return
                Get()
                .Where(r => r.PostId == PostId)
                .Select(r => r.ToViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }

        public bool IsLiked(string UserId, int PostId)
        {
            if (Get().Any(r => r.UserId == UserId && r.PostId == PostId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetReactsCountByPostId(int PostId)
        {
            return Get().Where(r => r.PostId == PostId).Count();
        }

        public void Delete(int ReactId)
        {
            React? React = Get(ReactId);
            if (React != null)
            {
                Delete(React);
            }
            else
            {
                throw new Exception("React Not Found");
            }
        }
    }
}