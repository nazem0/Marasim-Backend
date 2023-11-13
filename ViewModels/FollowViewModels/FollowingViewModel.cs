using System;
namespace ViewModels.FollowViewModels
{
    public class FollowingViewModel
    {
        public DateTime DateTime { get; set; }
        public required string Name { get; set; }
        public required string UserId { get; set; }
        public required string PicUrl { get; set; }
        public required int VendorId { get; set; }
    }
}

