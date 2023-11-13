using System;
using Models;
using ViewModels.PostViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.FollowViewModels
{
    public class FollowPostsViewModel
    {
        public VendorMidInfoViewModel? Vendor { get; set; }
        public IEnumerable<PostViewModel>? Posts { get; set; }
    }
}

