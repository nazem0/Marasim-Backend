using Application.DTOs.FollowDTOs;
using Application.DTOs.PaginationDTOs;
using Domain.Entities;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IFollowRepository
    {
        public PaginationDTO<VendorFollowerDTO> GetFollowersVendor(int vendorId, int pageIndex, int pageSize);
        public PaginationDTO<CustomerFollowingDTO> GetFollowingForUser(string userId, int pageIndex, int pageSize);
        public bool CheckUserFollowingVendor(string userId, int vendorId);
        public Follow? GetFollow(string userId, int vendorId);
        public HttpStatusCode Follow(CreateFollowDTO follow, string loggedInUserId);
        public HttpStatusCode Unfollow(string userId, int vendorId);
    }
}