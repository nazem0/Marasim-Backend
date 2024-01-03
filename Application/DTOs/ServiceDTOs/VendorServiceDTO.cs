using Application.DTOs.PromoCodeDTOs;
using Application.DTOs.ServiceAttachmentsDTOs;

namespace Application.DTOs.ServiceDTOs
{
    public class VendorServiceDTO
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int VendorId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
        public int ReviewsCount { get; set; }
        public required int ReservationsCount { get; set; }
        public required double AverageRate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual required IEnumerable<ServiceAttachmentDTO> ServiceAttachments { set; get; }
        public virtual PromoCodeDTO? PromoCode { set; get; }
    }
}
