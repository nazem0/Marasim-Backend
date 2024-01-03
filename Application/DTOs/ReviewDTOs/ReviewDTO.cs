using Application.DTOs.CustomerDTOs;

namespace Application.DTOs.ReviewDTOs
{
    public class ReviewDTO
    {
        public int Rate { get; set; }
        public required string Message { get; set; }
        public DateTime DateTime { get; set; }
        public required CustomerMinInfoDTO User { get; set; }
    }
}
