using Application.DTOs.VendorDTOs;

namespace Application.DTOs.CategoryDTOs
{
    public class CategoryWithVendorsDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<VendorMidInfoDTO>? Vendors { set; get; }
    }
}

