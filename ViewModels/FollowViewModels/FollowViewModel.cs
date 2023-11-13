﻿namespace ViewModels.FollowViewModels
{
    public class FollowViewModel
    {
        public DateTime DateTime { get; set; }
        public required string Name { get; set; }
        public required string UserId { get; set; }
        public required string PicUrl { get; set; }
    }
}
