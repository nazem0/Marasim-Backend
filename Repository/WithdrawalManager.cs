using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels.PaginationViewModels;
using ViewModels.WithdrawalViewModels;

namespace Repository
{
    public class WithdrawalManager : MainManager<Withdrawal>
    {
        private readonly EntitiesContext EntitiesContext;
        private readonly PaymentManager PaymentManager;
        public WithdrawalManager(EntitiesContext _dBContext, EntitiesContext entitiesContext, PaymentManager _PaymentManager) : base(_dBContext)
        {
            EntitiesContext = entitiesContext;
            PaymentManager = _PaymentManager;
        }

        public PaginationViewModel<WithdrawalViewModel> GetWithdrawals(int PageSize, int PageIndex)
        {
            PaginationDTO<WithdrawalViewModel> PaginationDTO = new()
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            return Get()
                .Select(w => w.ToWithdrawalViewModel())
                .ToPaginationViewModel(PaginationDTO);
        }

        public async Task<bool> AddAsync(AddWithdrawlViewModel Data, int VendorId)
        {
            ICollection<Payment> Payments = await PaymentManager.Get().Where(p => p.Reservation.Service.VendorId == VendorId && p.IsWithdrawn == false).ToListAsync();
            EntityEntry<Withdrawal> x = EntitiesContext.Add(Data.ToWithdrawal(VendorId, Payments));
            if (x.State != EntityState.Added) return false;
            else
            {
                foreach (var Payment in Payments)
                {
                    PaymentManager.IsWithdrawn(Payment.Id);
                }
                Save();
                return true;
            }
        }
        public bool ConfirmWithdrawal(int WithdrawalId)
        {
            Withdrawal withdrawal = Get(WithdrawalId)!;
            withdrawal.IsConfirmed = true;
            EntityEntry<Withdrawal> x = EntitiesContext.Update(withdrawal);
            if (x.State != EntityState.Modified) return false;
            else
            {
                Save();
                return true;
            }
        }
    }
}