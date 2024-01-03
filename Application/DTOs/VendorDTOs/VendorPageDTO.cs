using Application.DTOs.CityDTOs;
using Application.DTOs.FollowDTOs;
using Application.DTOs.GovernorateDTOs;
using Application.DTOs.PostDTOs;
using Application.DTOs.ServiceDTOs;

namespace Application.DTOs.VendorDTOs
{
    public class VendorPageDTO
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
        public required IEnumerable<CustomerServiceDTO> Services { get; set; }
        public required IEnumerable<PostDTO> Posts { get; set; }
        public required IEnumerable<VendorFollowerDTO> Followers { get; set; }
        public virtual required CityDTO City { get; set; }
        public virtual required GovernorateDTO Governorate { get; set; }
    }
}

