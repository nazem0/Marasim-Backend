using Application.DTOs.PaymentDTOs;
using Application.DTOs.VendorDTOs;

namespace Application.DTOs.WithdrawalDTOs
{
    public class WithdrawalDTO
    {
        public required int Id { get; set; }
        public required string InstaPay { get; set; }
        public required DateTime DateTime { get; set; }
        public required bool IsConfirmed { get; set; }
        public required float TotalWithdrawal { get; set; }
        public required IEnumerable<PaymentDTO> Payments { get; set; }
        public required VendorMinInfoDTO Vendor { get; set; }

    }
}

