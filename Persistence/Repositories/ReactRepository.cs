using Application.DTOs.PaginationDTOs;
using Application.DTOs.ReactDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class ReactRepository : IReactRepository
    {
        private readonly DbSet<React> _reacts;
        private readonly IUnitOfWork _unitOfWork;
        public ReactRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _reacts = entitiesContext.Reacts;
            _unitOfWork = unitOfWork;
        }
        public HttpStatusCode Add(CreateReactDTO createReactDTO, string loggedInUserId)
        {
            if (IsLiked(loggedInUserId, createReactDTO.PostId)) return HttpStatusCode.BadRequest;
            _reacts.Add(createReactDTO.ToEntity(loggedInUserId));
            return _unitOfWork.SaveChanges();
        }
        public PaginationDTO<ReactDTO> GetByPostId(int PostId, int PageIndex, int PageSize)
        {
            return
                _reacts
                .Where(r => r.PostId == PostId)
                .OrderByDescending(r => r.DateTime)
                .Select(r => r.ToReactDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        public bool IsLiked(string UserId, int PostId)
        {
            return _reacts.Any(r => r.UserId == UserId && r.PostId == PostId);
        }
        public int GetReactsCountByPostId(int PostId)
        {
            return _reacts.Where(r => r.PostId == PostId).Count();
        }

        public HttpStatusCode Delete(int ReactId)
        {
            React? React = _reacts.Find(ReactId);
            if (React is null) return HttpStatusCode.NotFound;

            _reacts.Remove(React);
            return _unitOfWork.SaveChanges();

        }
        public HttpStatusCode DeleteByPostId(int postId, string userId)
        {
            React? React = _reacts.Where(p => p.PostId == postId && p.UserId == userId).FirstOrDefault();
            if (React is null) return HttpStatusCode.NotFound;

            _reacts.Remove(React);
            return _unitOfWork.SaveChanges();

        }
    }
}