using Application.DTOs.FollowDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class FollowExtensions
    {
        public static Follow ToEntity(this CreateFollowDTO createFollowDTO, string loggedInUserId)
        {
            return new Follow
            {
                UserId = loggedInUserId,
                VendorId = createFollowDTO.VendorId,
                DateTime = DateTime.Now
            };
        }

        public static VendorFollowerDTO ToVendorFollowerDTO(this Follow Follow)
        {
            return new VendorFollowerDTO
            {
                Name = Follow.User.Name,
                UserId = Follow.User.Id,
                PicUrl = Follow.User.PicUrl,
                DateTime = Follow.DateTime,
            };
        }

        public static CustomerFollowingDTO ToCustomerFollowingDTO(this Follow Follow)
        {
            return new CustomerFollowingDTO
            {
                Name = Follow.Vendor.User.Name,
                PicUrl = Follow.Vendor.User.PicUrl,
                UserId = Follow.Vendor.UserId,
                VendorId = Follow.Vendor.Id,
                DateTime = Follow.DateTime
            };
        }

        public static FollowingPostsDTO ToFollowPostsViewModel(this Follow Follow)
        {
            return new FollowingPostsDTO
            {
                Vendor = Follow.Vendor.ToVendorMinInfoDTO(),
                Posts = Follow.Vendor.Posts.Select(p => p.ToPostDTO())
            };
        }
    }
}



