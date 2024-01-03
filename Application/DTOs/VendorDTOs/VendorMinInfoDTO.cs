namespace Application.DTOs.VendorDTOs
{
    public class VendorMinInfoDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PicUrl { get; set; }
        public required string UserId { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
