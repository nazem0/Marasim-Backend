using System;
using Models;
using ViewModels.PostViewModels;
using ViewModels.ServiceViewModels;

namespace ViewModels.VendorViewModels
{
    public class VendorFullViewModel
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string Summary { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public required string Address { get; set; }
        public required int CategoryId { get; set; }
        public required string ExternalUrl { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required bool Gender { get; set; }
        public required string NationalId { get; set; }
        public required IEnumerable<ServicePartialViewModel> Services { get; set; }
        public required IEnumerable<PostViewModel> Posts { get; set; }
    }
}

