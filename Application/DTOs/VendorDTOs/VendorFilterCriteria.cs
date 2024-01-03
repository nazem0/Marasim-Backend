namespace Application.DTOs.VendorDTOs
{
    public class VendorFilterCriteria
    {
        public string? Name { get; set; }
        public int? CityId { get; set; }
        public int? GovernorateId { get; set; }
        public string? District { get; set; }
        public string? Categories { get; set; }
        public int? Rate { get; set; }
    }
}
