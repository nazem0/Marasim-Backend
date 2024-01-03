namespace Application.DTOs.VendorDTOs
{
    public class VendorMidInfoDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required string UserId { get; set; }
        public required string Summary { get; set; }
        public required string Category { get; set; }
        public required DateTime RegistrationDate { get; set; }
        public required string City { get; set; }
        public required string District { get; set; }
        public required string Governorate { get; set; }
        public string? Street { get; set; }
    }
}
