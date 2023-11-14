using System;
using Models;
using ViewModels.CityViewModels;
using ViewModels.FollowViewModels;
using ViewModels.GovernorateViewModels;
using ViewModels.PostViewModels;
using ViewModels.ServiceViewModels;

namespace ViewModels.VendorViewModels
{
    public class VendorFullViewModel
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Summary { get; set; }
        public decimal? Latitude { get; set; } = null;
        public decimal? Longitude { get; set; } = null;
        public string Street { get; set; } = string.Empty;
        public required string District { get; set; }
        public required int CategoryId { get; set; }
        public required string ExternalUrl { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required bool Gender { get; set; }
        public required string NationalId { get; set; }
        public required string PhoneNumber { get; set; }
        public required IEnumerable<ServicePartialViewModel> Services { get; set; }
        public required IEnumerable<PostViewModel> Posts { get; set; }
        public required IEnumerable<FollowViewModel> Followers { get; set; }
        public virtual required CityViewModel City { get; set; }
        public virtual required GovernorateViewModel Governorate { get; set; }
    }
}

