using Application.DTOs.ServiceAttachmentsDTOs;

namespace Application.DTOs.ServiceDTOs
{
    public class CustomerServiceDTO
    {
        public required int Id { get; set; }
        public required string UserId { get; set; }
        public required int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }
        public required int ReviewsCount { get; set; }
        public required int ReservationsCount { get; set; }
        public required double AverageRate { get; set; }
        public required IEnumerable<ServiceAttachmentDTO> ServiceAttachments { set; get; }
    }
}

