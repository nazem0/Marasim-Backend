using Application.DTOs.WithdrawalDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class WithdrawalExtensions
    {
        public static Withdrawal ToEntity(this CreateWithdrawalDTO createWithdrawalDTO, int loggedInVendorId, ICollection<Payment> payments)
        {
            return new Withdrawal
            {
                InstaPay = createWithdrawalDTO.Instapay,
                VendorId = loggedInVendorId,
                DateTime = DateTime.Now,
                Payments = payments
            };
        }
        public static WithdrawalDTO ToWithdrawalDTO(this Withdrawal withdrawal)
        {
            return new WithdrawalDTO
            {
                Id = withdrawal.Id,
                InstaPay = withdrawal.InstaPay,
                DateTime = withdrawal.DateTime,
                IsConfirmed = withdrawal.IsConfirmed,
                TotalWithdrawal = (float)withdrawal.Payments.Sum(p => p.Amount),
                Payments = withdrawal.Payments.Select(p => p.ToPaymentDTO()),
                Vendor = withdrawal.Vendor.ToVendorMinInfoDTO()
            };
        }
    }
}