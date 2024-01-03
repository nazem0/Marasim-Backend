
using Application.DTOs.CustomerDTOs;

namespace Application.DTOs.PaymentDTOs
{
    public class VendorPaymentDTO
    {
        public required float Amount { get; set; }
        public required DateTime DateTime { get; set; }
        public required string ServiceTitle { get; set; }
        public required CustomerMinInfoDTO User { get; set; }

    }
}
