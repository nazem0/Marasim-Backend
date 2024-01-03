using Application.DTOs.CustomerDTOs;

namespace Application.DTOs.ReviewDTOs
{
    public class ReviewWithServiceDTO
    {
        public required int Id { get; set; }
        public required int Rate { get; set; }
        public required string Message { get; set; }
        public required DateTime DateTime { get; set; }
        public required string ServiceTitle { get; set; }
        public required int ServiceId { get; set; }
        public required CustomerMinInfoDTO User { get; set; }
    }
}

