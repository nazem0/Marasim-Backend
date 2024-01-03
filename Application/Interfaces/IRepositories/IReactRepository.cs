using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReactDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IReactRepository
    {
        public HttpStatusCode Add(CreateReactDTO createReactDTO, string loggedInUserId);
        public PaginationDTO<ReactDTO> GetByPostId(int PostId, int PageIndex, int PageSize);
        public bool IsLiked(string UserId, int PostId);
        public int GetReactsCountByPostId(int PostId);
        public HttpStatusCode Delete(int ReactId);
        public HttpStatusCode DeleteByPostId(int postId, string userId);
    }
}