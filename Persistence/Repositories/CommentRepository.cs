using Application.DTOs.CommentDTOs;
using Application.DTOs.PaginationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DbSet<Comment> _comments;
        private readonly IUnitOfWork _unitOfWork;

        public CommentRepository(AppDbContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _comments = entitiesContext.Comments;
            _unitOfWork = unitOfWork;
        }
        public PaginationDTO<CommentDTO> Get(int postId, int pageIndex, int pageSize)
        {
            return _comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.DateTime)
                .Select(c => c.ToCommentDTO()).ToPaginationDTO(pageIndex, pageSize);
        }
        public int GetCommentsCount(int postId)
        {
            return _comments.Where(c => c.PostId == postId).Count();
        }
        public HttpStatusCode Add(CreateCommentDTO comment, string loggedInUser)
        {
            Comment createdComment = comment.ToEntity(loggedInUser);
            _comments.Add(createdComment);

            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Update(UpdateCommentDTO comment, string loggedInUserId)
        {
            Comment? currentComment = _comments.Find(comment.Id);
            if (currentComment is null) return HttpStatusCode.NotFound;
            if (currentComment.UserId != loggedInUserId)
                currentComment.Text = comment.Text;
            _comments.Update(currentComment);
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Delete(int id, string loggedInUserId)
        {
            Comment? comment = _comments.Find(id);
            if (comment is null) return HttpStatusCode.NotFound;
            if (comment.UserId != loggedInUserId) return HttpStatusCode.Forbidden;
            _comments.Remove(comment);
            return _unitOfWork.SaveChanges();
        }
    }
}

