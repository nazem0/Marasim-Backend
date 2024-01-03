using Application.DTOs.CommentDTOs;
using Application.DTOs.PaginationDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface ICommentRepository
    {
        public PaginationDTO<CommentDTO> Get(int postId, int pageIndex, int pageSize);
        public int GetCommentsCount(int postId);
        public HttpStatusCode Add(CreateCommentDTO comment, string loggedInUser);
        public HttpStatusCode Update(UpdateCommentDTO comment, string loggedInUserId);
        public HttpStatusCode Delete(int id, string loggedInUserId);
    }
}

