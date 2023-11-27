using System;
using Models;
using ViewModels.PaymentViewModels;
using ViewModels.ReservationViewModels;
using ViewModels.VendorViewModels;

namespace ViewModels.WithdrawalViewModels
{
    public static class WithdrawlExtensions
    {
        public static Withdrawal ToWithdrawal(this AddWithdrawlViewModel Data, int VendorId, ICollection<Payment> Payments)
        {
            return new Withdrawal
            {
                InstaPay = Data.Instapay,
                VendorId = VendorId,
                DateTime = DateTime.Now,
                Payments = Payments
            };
        }
        public static WithdrawalViewModel ToWithdrawalViewModel(this Withdrawal Data)
        {
            return new WithdrawalViewModel
            {
                Id = Data.Id,
                InstaPay = Data.InstaPay,
                DateTime = Data.DateTime,
                IsConfirmed = Data.IsConfirmed,
                TotalWithdrawal = Data.Payments.Sum(p => p.Reservation.Price),
                Payments = Data.Payments.Select(p => p.ToPaymentViewModel()),
                Vendor = Data.Vendor.ToVendorMinInfoViewModel()
            };
        }
    }
}