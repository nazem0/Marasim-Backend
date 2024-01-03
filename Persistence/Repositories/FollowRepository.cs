using Application.DTOs.FollowDTOs;
using Application.DTOs.PaginationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly DbSet<Follow> _follows;
        private readonly IUnitOfWork _unitOfWork;

        public FollowRepository(EntitiesContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _follows = entitiesContext.Follows;
            _unitOfWork = unitOfWork;
        }

        public PaginationDTO<VendorFollowerDTO> GetFollowersVendor(int vendorId, int pageIndex, int pageSize)
        {
            return _follows
                .Where(f => f.VendorId == vendorId)
                .Select(f => f.ToVendorFollowerDTO())
                .ToPaginationDTO(pageIndex, pageSize);
        }

        public PaginationDTO<CustomerFollowingDTO> GetFollowingForUser(string userId, int pageIndex, int pageSize)
        {
            return _follows
                .Where(f => f.UserId == userId)
                .Select(f => f.ToCustomerFollowingDTO())
                .ToPaginationDTO(pageIndex, pageSize);
        }

        public bool CheckUserFollowingVendor(string userId, int vendorId)
        {
            return _follows.Any(f => f.UserId == userId && f.VendorId == vendorId);
        }

        public Follow? GetFollow(string userId, int vendorId)
        {
            return _follows.Where(f => f.UserId == userId && f.VendorId == vendorId).FirstOrDefault();

        }
        public HttpStatusCode Follow(CreateFollowDTO follow, string loggedInUserId)
        {
            var createdFollow = follow.ToEntity(loggedInUserId);

            if (CheckUserFollowingVendor(loggedInUserId, follow.VendorId))
                return HttpStatusCode.Conflict;

            _follows.Add(createdFollow);
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Unfollow(string userId, int vendorId)
        {
            Follow? follow = GetFollow(userId, vendorId);
            if (follow == null) return HttpStatusCode.BadRequest;
            _follows.Remove(follow);
            return _unitOfWork.SaveChanges();

        }
    }
}