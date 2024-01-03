using Application.DTOs.PostDTOs;
using Application.DTOs.VendorDTOs;

namespace Application.DTOs.FollowDTOs
{
    public class FollowingPostsDTO
    {
        public VendorMinInfoDTO? Vendor { get; set; }
        public IEnumerable<PostDTO>? Posts { get; set; }
    }
}

