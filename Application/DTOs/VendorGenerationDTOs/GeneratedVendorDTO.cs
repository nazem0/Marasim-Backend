using Application.DTOs.ServiceDTOs;

namespace Application.DTOs.VendorGenerationDTOs
{
    public class GeneratedVendorDTO
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; } = "لا يوجد";
        public string? PicUrl { get; set; } = null;
        public string? UserId { get; set; } = null;
        public string? Summary { get; set; } = null;
        public string? City { get; set; } = null;
        public string? District { get; set; } = null;
        public string? Governorate { get; set; } = null;
        public string? Street { get; set; } = null;
        public string? Category { get; set; } = null;
        public IEnumerable<CustomerServiceDTO>? Services { get; set; }
    }
}
